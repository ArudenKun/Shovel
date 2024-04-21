using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shovel.SourceGenerators.Extensions;
using Shovel.SourceGenerators.Utilities;

namespace Shovel.SourceGenerators.Generators;

[Generator]
internal sealed class ViewActivationGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(
            IsSyntaxTarget,
            GetSyntaxTarget
        );
        var compilationProvider = context.CompilationProvider.Combine(syntaxProvider.Collect());

        context.RegisterSourceOutput(
            compilationProvider,
            (productionContext, tuple) => Execute(productionContext, tuple.Left, tuple.Right)
        );
    }

    private void Execute(
        SourceProductionContext spc,
        Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> nodes
    )
    {
        var observableObjectSymbol = compilation.GetTypeByMetadataName(
            MetadataNames.ObservableObject
        );

        var viewModelSymbols = GetAll<INamedTypeSymbol>(nodes, compilation)
            .Where(x => x.IsOfBaseType(observableObjectSymbol))
            .Where(x => !x.IsAbstract)
            .ToArray();

        foreach (var viewModelSymbol in viewModelSymbols)
        {
            if (viewModelSymbol.ToDisplayString().Contains("Window"))
            {
                continue;
            }

            var viewSymbol = GetView(viewModelSymbol, compilation);

            if (viewSymbol is null)
            {
                continue;
            }

            var source = new SourceStringBuilder(viewSymbol);

            source.Line();
            source.Line("using Avalonia.Interactivity;");
            source.Line("using CommunityToolkit.Mvvm.DependencyInjection;");
            source.Line("using HanumanInstitute.MvvmDialogs;");
            source.Line();

            source.PartialTypeBlockBrace(() =>
            {
                source.Line($"private {viewModelSymbol.ToDisplayString()} _viewModel;");
                source.Line($"public {viewModelSymbol.ToDisplayString()} ViewModel =>");
                source.BlockTab(() =>
                {
                    source.Line(
                        $"({viewModelSymbol.ToDisplayString()})(DataContext = _viewModel ??= Ioc.Default.GetRequiredService<{viewModelSymbol.ToDisplayString()}>());"
                    );
                });

                source.Line("protected override void OnLoaded(RoutedEventArgs e)");
                source.BlockBrace(() =>
                {
                    source.Line("base.OnLoaded(e);");
                    source.Line("if (ViewModel is IViewLoaded viewLoaded)");
                    source.BlockBrace(() =>
                    {
                        source.Line("viewLoaded.OnLoaded();");
                    });

                    source.Line("OnLoaded();");
                });

                source.Line("partial void OnLoaded();");

                source.Line("protected override void OnUnloaded(RoutedEventArgs e)");
                source.BlockBrace(() =>
                {
                    source.Line("base.OnUnloaded(e);");
                    source.Line("if (ViewModel is IViewClosed viewClosed)");
                    source.BlockBrace(() =>
                    {
                        source.Line("viewClosed.OnClosed();");
                    });

                    source.Line("OnUnloaded();");
                });

                source.Line("partial void OnUnloaded();");
            });

            spc.AddSource($"{viewSymbol.ToDisplayString()}.g.cs", source.ToString());
        }
    }

    private static bool IsSyntaxTarget(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax;

    private static ClassDeclarationSyntax GetSyntaxTarget(
        GeneratorSyntaxContext context,
        CancellationToken _
    ) => (ClassDeclarationSyntax)context.Node;

    private IEnumerable<TSymbol> GetAll<TSymbol>(
        IEnumerable<SyntaxNode> syntaxNodes,
        Compilation compilation
    )
        where TSymbol : ISymbol
    {
        foreach (var syntaxNode in syntaxNodes)
        {
            if (syntaxNode is FieldDeclarationSyntax fieldDeclaration)
            {
                var semanticModel = compilation.GetSemanticModel(fieldDeclaration.SyntaxTree);

                foreach (var variable in fieldDeclaration.Declaration.Variables)
                {
                    if (semanticModel.GetDeclaredSymbol(variable) is not TSymbol symbol)
                    {
                        continue;
                    }

                    yield return symbol;
                }
            }
            else
            {
                var semanticModel = compilation.GetSemanticModel(syntaxNode.SyntaxTree);

                if (semanticModel.GetDeclaredSymbol(syntaxNode) is not TSymbol symbol)
                {
                    continue;
                }

                yield return symbol;
            }
        }
    }

    private static INamedTypeSymbol GetView(ISymbol symbol, Compilation compilation)
    {
        var viewName = symbol.ToDisplayString().Replace("ViewModel", "View");

        var viewSymbol = compilation.GetTypeByMetadataName(viewName);

        if (viewSymbol is not null)
        {
            return viewSymbol;
        }

        viewName = symbol.ToDisplayString().Replace(".ViewModels.", ".Views.");
        viewName = viewName.Remove(viewName.IndexOf("ViewModel", StringComparison.Ordinal));
        return compilation.GetTypeByMetadataName(viewName);
    }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Shovel.SourceGenerators.Attributes;
using Shovel.SourceGenerators.Extensions;
using Shovel.SourceGenerators.Utilities;

namespace Shovel.SourceGenerators.Generators;

[Generator]
internal sealed class StaticViewLocatorGenerator
    : SourceGeneratorForDeclaredTypeWithAttribute<StaticViewLocatorAttribute>
{
    protected override (string GeneratedCode, DiagnosticDetail Error) GenerateCode(
        Compilation compilation,
        SyntaxNode node,
        INamedTypeSymbol symbol,
        AttributeData attribute,
        AnalyzerConfigOptions options
    )
    {
        var observableObject = compilation.GetTypeByMetadataName(MetadataNames.ObservableObject);

        var viewModelSymbols = compilation
            .GetSymbolsWithName(x => x.EndsWith("ViewModel"))
            .OfType<INamedTypeSymbol>()
            .Where(x => x.IsOfBaseType(observableObject))
            .Where(x => !x.IsAbstract)
            .ToArray();

        var source = new SourceStringBuilder(symbol);

        source.Line();
        source.Line("using System;");
        source.Line("using System.Collections.Generic;");
        source.Line("using Avalonia.Controls;");
        source.Line("using HanumanInstitute.MvvmDialogs.Avalonia;");
        source.Line();

        source.PartialTypeBlockBrace(
            "StrongViewLocator",
            () =>
            {
                source.Constructor(() =>
                {
                    foreach (var viewModelSymbol in viewModelSymbols)
                    {
                        var viewSymbol = GetView(viewModelSymbol, compilation);

                        if (viewSymbol is null)
                        {
                            continue;
                        }

                        source.Line(
                            $"Register<{viewModelSymbol.ToFullDisplayString()}, {viewSymbol.ToFullDisplayString()}>();"
                        );
                    }
                });
            }
        );

        return (source.ToString(), null);
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

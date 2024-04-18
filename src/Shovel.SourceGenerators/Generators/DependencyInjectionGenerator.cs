using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Shovel.SourceGenerators.Attributes;
using Shovel.SourceGenerators.Extensions;
using Shovel.SourceGenerators.Utilities;

namespace Shovel.SourceGenerators.Generators;

[Generator]
internal sealed class DependencyInjectionGenerator
    : SourceGeneratorForDeclaredMember<ClassDeclarationSyntax>
{
    protected override (string FileName, string GeneratedCode) GenerateCode(
        Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> nodes,
        AnalyzerConfigOptions options
    )
    {
        var observableObject = compilation.GetTypeByMetadataName(MetadataNames.OBSERVABLE_OBJECT);

        var viewModelSymbols = GetAll<INamedTypeSymbol>(nodes)
            .Where(x => !x.IsAbstract)
            .Where(x => x.Name.EndsWith("ViewModel"))
            .Where(x => x.IsOfBaseType(observableObject))
            .Where(x => !x.IsAbstract)
            .ToArray();

        var source = new SourceStringBuilder();

        source.Line();
        source.Line("using Microsoft.Extensions.DependencyInjection;");
        source.Line();

        source.NamespaceBlockBrace(
            $"{MetadataNames.SHOVEL}.Core",
            () =>
            {
                source.Line("public static partial class ServiceCollectionExtensions");
                source.BlockBrace(() =>
                {
                    source.Line(
                        "static partial void AddViewsAndViewModels(IServiceCollection services)"
                    );
                    source.BlockBrace(() =>
                    {
                        foreach (var viewModelSymbol in viewModelSymbols)
                        {
                            var viewName = GetViewName(viewModelSymbol);
                            var viewSymbol = compilation.GetTypeByMetadataName(viewName);

                            if (viewSymbol is null)
                            {
                                continue;
                            }

                            source.Line(
                                viewModelSymbol.HasAttribute(nameof(SingletonAttribute))
                                    ? $"services.AddSingleton<{viewModelSymbol.ToFullDisplayString()}>();"
                                    : $"services.AddTransient<{viewModelSymbol.ToFullDisplayString()}>();"
                            );

                            if (viewModelSymbol.BaseType is { } baseType)
                            {
                                source.Line(
                                    viewModelSymbol.HasAttribute(nameof(SingletonAttribute))
                                        ? $"services.AddSingleton<{baseType.ToFullDisplayString()}>(sp => sp.GetRequiredService<{viewModelSymbol.ToFullDisplayString()}>());"
                                        : $"services.AddTransient<{baseType.ToFullDisplayString()}>(sp => sp.GetRequiredService<{viewModelSymbol.ToFullDisplayString()}>());"
                                );
                            }

                            source.Line(
                                viewModelSymbol.HasAttribute(nameof(SingletonAttribute))
                                    ? $"services.AddSingleton<{viewSymbol.ToFullDisplayString()}>();"
                                    : $"services.AddTransient<{viewSymbol.ToFullDisplayString()}>();"
                            );
                        }
                    });
                });
            }
        );

        return ($"{MetadataNames.SHOVEL}.Core.AddViewModels.g.cs", source.ToString());
    }

    private static string GetViewName(ISymbol symbol)
    {
        string[] words = ["Windows", "Page", "Dialog"];
        var name = symbol.ToDisplayString();

        if (!words.Any(word => name.Contains(word)))
        {
            return name.Replace("ViewModel", "View");
        }

        name = name.Replace(".ViewModels.", ".Views.");
        return name.Remove(name.IndexOf("ViewModel", StringComparison.Ordinal));
    }
}

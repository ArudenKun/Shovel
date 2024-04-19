using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Shovel.SourceGenerators.Attributes;
using Shovel.SourceGenerators.Utilities;
using Shovel.SourceGenerators.Extensions;

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

        source.PartialTypeBlockBrace("StrongViewLocator", () =>
        {
            source.Constructor(() =>
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
                        $"Register<{viewModelSymbol.ToFullDisplayString()}, {viewSymbol.ToFullDisplayString()}>();");
                }
            });
        });

        return (source.ToString(), null);
    }

    private static readonly string[] s_words = ["Window", "Dialog"];

    private static string GetViewName(ISymbol symbol)
    {
        var name = symbol.ToDisplayString();

        if (!s_words.Any(x => name.Contains(x)))
            return name.Replace("ViewModel", "View");

        name = name.Replace(".ViewModels.", ".Views.");
        return name.Remove(name.IndexOf("ViewModel", StringComparison.Ordinal));
    }
}
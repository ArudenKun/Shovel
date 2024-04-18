using Microsoft.CodeAnalysis;
using Shovel.SourceGenerators.Attributes;
using SourceGenerator.Helper.CopyCode;

namespace Shovel.SourceGenerators.Generators;

[Generator]
internal sealed class StaticFileGenerator : IIncrementalGenerator
{
    private const string USINGS_TEXT = """
        global using System;
        """;

    private const string SERVICE_COLLECTION_EXTENSIONS_TEXT = $$"""
        using Microsoft.Extensions.DependencyInjection;

        namespace {{MetadataNames.SHOVEL}}.Core;

        public static partial class ServiceCollectionExtensions
        {
            static partial void AddViewsAndViewModels(IServiceCollection services);

            public static IServiceCollection AddCore(this IServiceCollection services)
            {
                AddViewsAndViewModels(services);
                return services;
            }
        }
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource($"{MetadataNames.SHOVEL}.Core.Usings.g.cs", USINGS_TEXT);
            ctx.AddSource(
                $"{MetadataNames.SHOVEL}.Core.ServiceCollectionExtensions.g.cs",
                SERVICE_COLLECTION_EXTENSIONS_TEXT
            );
            ctx.AddSource(
                FileName(nameof(SingletonAttribute)),
                Copy.ShovelSourceGeneratorsAttributesSingletonAttribute
            );
            ctx.AddSource(
                FileName(nameof(IgnoreAttribute)),
                Copy.ShovelSourceGeneratorsAttributesIgnoreAttribute
            );
        });
    }

    private static string ParseAttributeName(string attribute) =>
        attribute.Replace("Attribute", "");

    private static string FileName(string attributeName) =>
        $"{MetadataNames.SHOVEL}.Core.Attributes.{ParseAttributeName(attributeName)}.g.cs";
}

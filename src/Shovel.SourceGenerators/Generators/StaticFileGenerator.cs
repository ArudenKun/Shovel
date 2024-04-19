using Microsoft.CodeAnalysis;
using Shovel.SourceGenerators.Attributes;
using SourceGenerator.Helper.CopyCode;

namespace Shovel.SourceGenerators.Generators;

[Generator]
internal sealed class StaticFileGenerator : IIncrementalGenerator
{
    private const string UsingsText = """
        global using System;
        """;

    private const string ServiceCollectionExtensionsText = $$"""
        using Microsoft.Extensions.DependencyInjection;

        namespace {{MetadataNames.AppName}}.Core;

        public static partial class ServiceCollectionExtensions
        {
            static partial void AddViewModels(IServiceCollection services);

            public static IServiceCollection AddCore(this IServiceCollection services)
            {
                AddViewModels(services);
                return services;
            }
        }
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource($"{MetadataNames.AppName}.Core.Usings.g.cs", UsingsText);
            ctx.AddSource(
                $"{MetadataNames.AppName}.Core.ServiceCollectionExtensions.g.cs",
                ServiceCollectionExtensionsText
            );
            ctx.AddSource(
                FileName(nameof(SingletonAttribute)),
                Copy.ShovelSourceGeneratorsAttributesSingletonAttribute
            );
            ctx.AddSource(
                FileName(nameof(StaticViewLocatorAttribute)),
                Copy.ShovelSourceGeneratorsAttributesStaticViewLocatorAttribute
            );
            ctx.AddSource(
                FileName(nameof(IgnoreAttribute)),
                Copy.ShovelSourceGeneratorsAttributesIgnoreAttribute
            );
            ctx.AddSource(
                FileName(nameof(ActivatableAttribute)),
                Copy.ShovelSourceGeneratorsAttributesActivatableAttribute
            );
        });
    }

    private static string ParseAttributeName(string attribute) =>
        attribute.Replace("Attribute", "");

    private static string FileName(string attributeName) =>
        $"{MetadataNames.AppName}.Core.Attributes.{ParseAttributeName(attributeName)}.g.cs";
}

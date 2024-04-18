using SourceGenerator.Helper.CopyCode;

namespace Shovel.SourceGenerators.Attributes;

[Copy]
[AttributeUsage(AttributeTargets.Class)]
public sealed class SingletonAttribute : Attribute;

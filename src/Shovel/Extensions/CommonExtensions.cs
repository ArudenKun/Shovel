namespace Shovel.Extensions;

public static class CommonExtensions
{
    public static T As<T>(this object obj) => (T)obj;
}

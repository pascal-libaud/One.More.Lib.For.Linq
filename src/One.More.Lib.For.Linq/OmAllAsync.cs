namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static async Task<bool> OmAllAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        await foreach (var item in source)
            if (!await predicate(item))
                return false;

        return true;
    }

    public static async Task<bool> OmAllAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        await foreach (var item in source)
            if (!predicate(item))
                return false;

        return true;
    }

    public static async Task<bool> OmAllAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        foreach (var item in source)
            if (!await predicate(item))
                return false;

        return true;
    }
}
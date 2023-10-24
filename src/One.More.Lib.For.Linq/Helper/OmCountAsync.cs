namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source)
    {
        int result = 0;
        await foreach (var _ in source)
            result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        int result = 0;
        await foreach (var t in source)
            if (await predicate(t))
                result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        int result = 0;
        await foreach (var t in source)
            if (predicate(t))
                result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        int result = 0;
        foreach (var t in source)
            if (await predicate(t))
                result++;

        return result;
    }
}
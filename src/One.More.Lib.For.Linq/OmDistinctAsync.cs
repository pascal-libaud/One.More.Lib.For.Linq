namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> OmDistinctAsync<T>(this IAsyncEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        await foreach (var item in source)
        {
            if (hash.Add(item))
                yield return item;
        }
    }
}
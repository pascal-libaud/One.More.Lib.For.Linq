namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async Task<List<T>> OmToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
            list.Add(item);

        return list;
    }
}
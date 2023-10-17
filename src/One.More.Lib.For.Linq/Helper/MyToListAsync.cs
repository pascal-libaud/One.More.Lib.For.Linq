namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async Task<List<T>> MyToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
            list.Add(item);

        return list;
    }
}
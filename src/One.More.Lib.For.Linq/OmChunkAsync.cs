namespace One.More.Lib.For.Linq; 

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T[]> OmChunkAsync<T>(this IAsyncEnumerable<T> source, int size)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
            if (list.Count == size)
            {
                yield return list.ToArray();
                list.Clear();
            }
        }

        if (list.Count > 0)
            yield return list.ToArray();
    }
}
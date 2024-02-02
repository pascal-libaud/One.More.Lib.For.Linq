namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T[]> OmChunk<T>(this IEnumerable<T> source, int size)
    {
        var list = new List<T>();
        foreach (var item in source)
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
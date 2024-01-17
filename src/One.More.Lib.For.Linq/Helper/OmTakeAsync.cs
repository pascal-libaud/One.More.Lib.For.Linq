namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IAsyncEnumerable<T> OmTakeAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => OmTakeAsync_Foreach(source, count),
            EnumerationWay.Enumerator => OmTakeAsync_Enumerator(source, count),
            _ => OmTakeAsync_Foreach(source, count)
        };
    }

    private static async IAsyncEnumerable<T> OmTakeAsync_Foreach<T>(this IAsyncEnumerable<T> source, int count)
    {
        if (count > 0)
            await foreach (var item in source)
            {
                yield return item;
                if (--count <= 0)
                    break;
            }
    }

    private static async IAsyncEnumerable<T> OmTakeAsync_Enumerator<T>(this IAsyncEnumerable<T> source, int count)
    {
        if (count > 0)
        {
            await using var enumerator = source.GetAsyncEnumerator();

            while (count-- > 0 && await enumerator.MoveNextAsync())
                yield return enumerator.Current;
        }
    }
}
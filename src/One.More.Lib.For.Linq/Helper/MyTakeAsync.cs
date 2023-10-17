namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> MyTakeAsync<T>(this IAsyncEnumerable<T> source, int count)
    {
        // Version foreach
        //if (count > 0)
        //    await foreach (var item in source)
        //    {
        //        yield return item;
        //        if (--count <= 0)
        //            break;
        //    }

        // Version Enumerator
        if (count > 0)
        {
            await using var enumerator = source.GetAsyncEnumerator();

            while (count-- > 0 && await enumerator.MoveNextAsync())
                yield return enumerator.Current;
        }
    }
}
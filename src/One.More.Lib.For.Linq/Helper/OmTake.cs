namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmTake<T>(this IEnumerable<T> source, int count)
    {
        // Version foreach
        //if (count > 0)
        //    foreach (var item in source)
        //    {
        //        yield return item;
        //        if (--count <= 0)
        //            break;
        //    }

        // Version Enumerator
        if (count > 0)
        {
            using var enumerator = source.GetEnumerator();

            while (count-- > 0 && enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}
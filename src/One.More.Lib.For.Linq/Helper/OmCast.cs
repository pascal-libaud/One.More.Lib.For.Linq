using System.Collections;

namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmCast<TResult>(this IEnumerable source)
    {
        foreach (var item in source)
            yield return (TResult)item;
    }
}
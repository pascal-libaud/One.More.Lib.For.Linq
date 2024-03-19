using System.Collections;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmOfType<TResult>(this IEnumerable source)
    {
        foreach (var item in source)
            if (item is TResult result)
                yield return result;
    }
}
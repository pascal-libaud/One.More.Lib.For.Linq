namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
    {
        return outer.OmJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
    }

    public static IEnumerable<TResult> OmJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
    {
        ICollection<TInner>? innerCollection = null;
        var func = () => innerCollection ??= inner as ICollection<TInner> ?? inner.OmToList();
        comparer ??= EqualityComparer<TKey>.Default;

        foreach (var o in outer)
            foreach (var i in func())
            {
                if (comparer.Equals(outerKeySelector(o), innerKeySelector(i)))
                    yield return resultSelector(o, i);
            }
    }
}
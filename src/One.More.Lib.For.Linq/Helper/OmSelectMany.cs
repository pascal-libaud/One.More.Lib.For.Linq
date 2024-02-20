namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmSelectMany<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
    {
        foreach (var item in source)
            foreach (var result in selector(item))
                yield return result;
    }

    // TODO Rajouter le support de l'index, ainsi qu'en version Async
    //public static IEnumerable<TResult> OmSelectMany<T, TResult>(this IEnumerable<T> source, Func<T, int, IEnumerable<TResult>> selector)
    //{ }

    public static IEnumerable<TResult> OmSelectMany<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        foreach (var item in source)
            foreach (var result in collectionSelector(item))
                yield return resultSelector(item, result);
    }

    //public static IEnumerable<TResult> OmSelectMany<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, int, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    //{ }
}
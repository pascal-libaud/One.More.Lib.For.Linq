namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmReverse<T>(this IEnumerable<T> source)
    {
        if (source is IList<T> list)
            return new EnumerableWithCount<T>(list.InnerOmReverse(), () => list.Count);

        if( source is ICollection<T> collection)
            return new EnumerableWithCount<T>(source.InnerOmReverse(), () => collection.Count);

        if(source is IWithCount withCount)
            return new EnumerableWithCount<T>(source.InnerOmReverse(), () => withCount.Count);

        return source.InnerOmReverse();
    }

    private static IEnumerable<T> InnerOmReverse<T>(this IEnumerable<T> source)
    {
        Stack<T> stack = new Stack<T>();
        foreach (var item in source)
            stack.Push(item);

        while (stack.Count > 0)
            yield return stack.Pop();
    }

    private static IEnumerable<T> InnerOmReverse<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
            yield return list[i];
    }
}
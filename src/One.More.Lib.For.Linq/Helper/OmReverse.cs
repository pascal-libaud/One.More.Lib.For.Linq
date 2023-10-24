namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmReverse<T>(this IEnumerable<T> source)
    {
        Stack<T> stack = new Stack<T>();
        foreach (var item in source)
            stack.Push(item);

        while (stack.Count > 0)
            yield return stack.Pop();
    }
}
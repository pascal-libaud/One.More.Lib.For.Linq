namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> OmReverseAsync<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        Stack<T> stack = new Stack<T>();
        await foreach (var item in source.WithCancellation(cancellationToken))
            stack.Push(item);

        while (stack.Count > 0)
            yield return stack.Pop();
    }
}
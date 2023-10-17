namespace One.More.Lib.For.Linq;

public static class LogHelper
{
    public static IEnumerable<T> Log<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            Console.WriteLine($"Log: {item}");
            yield return item;
        }
    }
}
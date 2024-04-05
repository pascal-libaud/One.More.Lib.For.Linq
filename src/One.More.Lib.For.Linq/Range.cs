using System.Reflection.Metadata;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> Range<T>(T count) where T : INumber<T>
    {
        return new EnumerableWithCount<T>(InnerRange(T.Zero, count), () => int.CreateChecked(count));
    }

    public static IEnumerable<T> Range<T>(T start, T count) where T : INumber<T>
    {
        return new EnumerableWithCount<T>(InnerRange(start, count), () => int.CreateChecked(count));
    }

    public static IEnumerable<T?> RangeNullable<T>(T count) where T : struct, INumber<T>
    {
        return new EnumerableWithCount<T?>(InnerRangeNullable(T.Zero, count), () => int.CreateChecked(count));
    }

    public static IEnumerable<T?> RangeNullable<T>(T start, T count) where T : struct, INumber<T>
    {
        return new EnumerableWithCount<T?>(InnerRangeNullable(start, count), () => int.CreateChecked(count));
    }

    private static IEnumerable<T> InnerRange<T>(T start, T count) where T : INumber<T>
    {
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }

    private static IEnumerable<T?> InnerRangeNullable<T>(T start, T count) where T : struct, INumber<T>
    {
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }
}
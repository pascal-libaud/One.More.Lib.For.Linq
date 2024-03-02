using System.Linq;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<(T, U)> OmCrossJoin<T, U>(this IEnumerable<T> source1, IReadOnlyCollection<U> source2)
    {
        foreach (var item1 in source1)
        foreach (var item2 in source2)
            yield return (item1, item2);
    }

    //TODO Mettre au propre
    public static IEnumerable<(T, U)> OmCrossJoinBis<T, U>(this IEnumerable<T> source1, IReadOnlyCollection<U> source2)
    {
        return source1.SelectMany(_ => source2, (x, y) => (x, y));
    }
}
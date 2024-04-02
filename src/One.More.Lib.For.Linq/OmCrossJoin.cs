using System.Linq;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<(TFirst, TSecond)> OmCrossJoin<TFirst, TSecond>(this IEnumerable<TFirst> source1, IReadOnlyCollection<TSecond> source2)
    {
        foreach (var item1 in source1)
        foreach (var item2 in source2)
            yield return (item1, item2);
    }

    //TODO Mettre au propre
    public static IEnumerable<(TFirst, TSecond)> OmCrossJoinBis<TFirst, TSecond>(this IEnumerable<TFirst> source1, IReadOnlyCollection<TSecond> source2)
    {
        return source1.SelectMany(_ => source2, (x, y) => (x, y));
    }
}
using System.Collections;

namespace One.More.Lib.For.Linq.Tests;

public class SpyReadOnlyList<T> : IReadOnlyList<T>, ISpyEnumerable<T>
{
    private readonly IReadOnlyList<T> _list;

    public SpyReadOnlyList(IReadOnlyList<T> list)
    {
        _list = list;
    }

    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; set; } = false;
    public int CountEnumeration { get; set; } = 0;
    public int CountItemEnumerated { get; set; } = 0;

    public IEnumerator<T> GetEnumerator()
    {
        return new SpyEnumerator<T>(this, _list.GetEnumerator());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _list.Count;

    public T this[int index]
    {
        get
        {
            CountItemEnumerated++;
            return _list[index];
        }
    }
}

public class SpyReadOnlyListTest : TestBase
{
    [Fact]
    public void SpyReadOnlyList_should_spy_enumerations_as_expected()
    {
        var spyList = new SpyReadOnlyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        Assert.Equal(0, spyList.CountEnumeration);
        Assert.Equal(0, spyList.CountItemEnumerated);

        foreach (var _ in spyList)
        {
            break;
        }

        Assert.Equal(1, spyList.CountEnumeration);
        Assert.Equal(1, spyList.CountItemEnumerated);

        foreach (var _ in spyList) { }

        Assert.Equal(2, spyList.CountEnumeration);
        Assert.Equal(11, spyList.CountItemEnumerated);

        using var enumerator = spyList.GetEnumerator();
        while (enumerator.MoveNext())
        {
            _ = enumerator.Current;
            break;
        }
        Assert.Equal(3, spyList.CountEnumeration);
        Assert.Equal(12, spyList.CountItemEnumerated);

        using var enumerator2 = spyList.GetEnumerator();
        while (enumerator2.MoveNext())
        {
            _ = enumerator2.Current;
        }
        Assert.Equal(4, spyList.CountEnumeration);
        Assert.Equal(22, spyList.CountItemEnumerated);

        for (int i = 0; i < spyList.Count; i++)
        {
            _ = spyList[i];
        }

        // Malheureusement, il n'est pas possible d'espionner énumérations juste l'accés aux valeurs
        Assert.Equal(4, spyList.CountEnumeration);
        Assert.Equal(32, spyList.CountItemEnumerated);
    }
}
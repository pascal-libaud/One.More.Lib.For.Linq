using System.Collections;

namespace One.More.Lib.For.Linq.Tests;

public class SpyList<T> : IList<T>, ISpyEnumerable<T>
{
    private readonly IList<T> _list;

    public SpyList(IList<T> list)
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

    public void Add(T item)
    {
        _list.Add(item);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public bool Contains(T item)
    {
        return _list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _list.Remove(item);
    }

    public int Count => _list.Count;

    public bool IsReadOnly => _list.IsReadOnly;

    public int IndexOf(T item)
    {
        return _list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        _list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);
    }


    public T this[int index]
    {
        get
        {
            CountItemEnumerated++;
            return _list[index];
        }
        set => _list[index] = value;
    }
}

public class SpyListTest : TestBase
{
    [Fact]
    public void SpyList_should_spy_enumerations_as_expected()
    {
        var spyList = new SpyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

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
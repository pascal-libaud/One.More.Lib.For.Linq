namespace One.More.Lib.For.Linq.Tests;

public class OmCountTest : TestBase
{
    [Fact]
    public void OmCount_should_work_as_expected()
    {
        Assert.Equal(10, SpyEnumerable.GetValues().OmCount());
    }

    [Fact]
    public void OmCount_should_enumerate_only_once()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmCount();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public void OmCount_should_not_enumerate_when_not_needed()
    {
        var spy = new SpyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        _ = spy.OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        // Linq IList         => CountEnumeration : 0
        // Linq IReadOnlyList => CountEnumeration : 1
        // LinqHelper (all)   => CountEnumeration : 0

        // Linq       CountItemEnumerated = 10 // certains devs se servent du Count() pour tout énumérer sans faire de montée en mémoire contrairement à .ToList()
        // LinqHelper CountItemEnumerated = 0  // LinqHelper propose uniquement la version optimisée, pour tout évaluer, préférer utiliser ...(todo)

        _ = spy.OmSelect(x => x.ToString()).OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        _ = spy.OmAppend(10).OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        _ = spy.OmCast<int>().OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);
    }

    [Fact]
    public void OmCount_on_OnTake_should_be_correct()
    {
        Assert.Equal(5, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.OmTake(5).OmCount());
        Assert.Equal(2, new List<int> { 0, 1 }.OmTake(5).OmCount());
    }

    [Fact]
    public void OmCount_should_not_enumerate_when_methods_combined()
    {
        var spy = new SpyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        _ = spy.OmPrepend(-1)
            .OmAppend(10)
            .OmSelect(x => 2 * x)
            .OmCast<int>()
            .OmTake(5)
            .OmReverse()
            .OmSkip(1)
            .OmSkipLast(1)
            .OmCount();

        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);
    }
}
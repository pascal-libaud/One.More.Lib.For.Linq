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

        // Linq       CountItemEnumerated = 10 // certains devs se servent du Count() pour tout �num�rer sans faire de mont�e en m�moire contrairement � .ToList()
        // LinqHelper CountItemEnumerated = 0  // LinqHelper propose uniquement la version optimis�e, pour tout �valuer, pr�f�rer utiliser ...(todo)

        _ = spy.OmSelect(x => x.ToString()).OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);
    }
}
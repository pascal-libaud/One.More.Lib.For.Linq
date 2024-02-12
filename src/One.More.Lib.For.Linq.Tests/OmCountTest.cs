using System.Linq;
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmCountTest : TestBase
{
    [Fact]
    public void OmCount_should_work_as_expected()
    {
        var spy = new SpyEnumerable();

        Assert.Equal(10, spy.GetValues().OmCount());
    }

    [Fact]
    public void OmCount_should_enumerate_only_once()
    {
        var spy = new SpyEnumerable();

        _ = spy.GetValues().OmCount();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public void OmCount_should_not_enumerate_when_not_needed()
    {
        var spy = new SpyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        _ = spy.OmCount();
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        // TODO : Voir ce qu'on en fait car le comportement du .Count() diffère si c'est une IList ou une IReadOnlyList
        // IList => CountEnumeration : 0
        // IReadOnlyList => CountEnumeration : 1
        // De plus, CountItemEnumerated = 10 car les devs se servent du Count() pour tout énumérer sans faire de montée en mémoire contrairement à .ToList()

        //_ = spy.Select(x => x.ToString()).Count();
        //Assert.Equal(0, spy.CountEnumeration);
        //Assert.Equal(0, spy.CountItemEnumerated);
    }
}
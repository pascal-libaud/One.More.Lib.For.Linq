using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmSelectThrowEarlyTest : TestBase
{
    [Fact]
    public void OmSelectThrowEarly_should_throw_when_empty_before_enumeration()
    {
        IEnumerable<int> source = LinqHelper.Empty<int>();
        Assert.Throws<InvalidOperationException>(() => source.OmSelectThrowEarly(x => x.ToString()));
    }

    [Fact]
    public void OmSelectThrowEarly_should_work_well_when_not_empty()
    {
        var source = LinqHelper.Range(5).OmSelectThrowEarly(x => x * 2);
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }

    [Fact]
    public void OmSelectThrowEarly_should_not_enumerate_twice()
    {
        var spy = new SpyEnumerable();

        var source = spy.GetValues().OmSelectThrowEarly(x => x.ToString());

        // Assert.Equal(1, countEnumerations); // A ne pas tester ici car ce n'est pas ce qu'on cherche à vérifier

        _ = source.OmTake(5).OmToList();

        Assert.Equal(1, spy.CountEnumeration);
    }
}
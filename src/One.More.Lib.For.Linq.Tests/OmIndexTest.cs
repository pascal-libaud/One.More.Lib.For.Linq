#pragma warning disable CS8604 // Possible null reference argument.
namespace One.More.Lib.For.Linq.Tests;

public class OmIndexTest : TestBase
{
    [Fact]
    public void OmIndex_should_not_enumerate_early()
    {
        var omIndex = (IEnumerable<int> x) => x.OmIndex();
        omIndex.Should_not_enumerate_early();
    }

    [Fact]
    public void OmIndex_should_enumerate_only_on_demand()
    {
        var spy = SpyEnumerable.GetValues();

        foreach (var (index, _) in spy.OmIndex())
        {
            if (index == 5)
                break;
        }

        Assert.Equal(6, spy.CountItemEnumerated);
    }

    [Fact]
    public void OmIndex_should_enumerate_each_item_once()
    {
        var omIndex = (IEnumerable<int> x) => x.OmIndex().OmToList();
        omIndex.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmIndex_should_not_make_stack_overflow()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmIndex().OmTake(5).OmToList();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void OmIndex_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IEnumerable<int>? enumerable = null;
            _ = enumerable.OmIndex();
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public void OmIndex_should_throw_when_null_on_enumeration()
    {
        var func = () =>
        {
            IEnumerable<int>? enumerable = null;
            foreach (var _ in enumerable.OmIndex())
            { }
        };

        Assert.Throws<NullReferenceException>(func);
    }

    [Fact]
    public void OmIndex_should_work_well_when_not_null()
    {
        var source = LinqHelper.Range(5).OmSelect(x => x * 2).OmIndex();
        var expected = new[] { (0, 0), (1, 2), (2, 4), (3, 6), (4, 8) };

        Assert.Equal(expected, source);
    }
}
namespace One.More.Lib.For.Linq.Tests;

public class OmSelectTest : TestBase
{
    [Fact]
    public void OmSelect_should_not_enumerate_early()
    {
        var omSelect = (IEnumerable<int> x) => x.OmSelect(y => y.ToString());
        omSelect.Should_not_enumerate_early();
    }

    [Fact]
    public void OmSelect_should_enumerate_only_on_demand()
    {
        var spy = SpyEnumerable.GetValues();

        foreach (var value in spy.OmSelect(x => x))
        {
            if(value == 5)
                break;
        }

        Assert.Equal(6, spy.CountItemEnumerated);
    }

    [Fact]
    public void OmSelect_should_enumerate_each_item_once()
    {
        var omSelect = (IEnumerable<int> x) => x.OmSelect(z => z.ToString()).OmToList();
        omSelect.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmSelect_should_not_make_stack_overflow()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmSelect(x => x.ToString()).OmTake(5).OmToList();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void OmSelect_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IEnumerable<int>? enumerable = null;
            _ = enumerable.OmSelect(x => x.ToString());
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public void OmSelect_should_throw_when_null_on_enumeration()
    {
        var func = () =>
        {
            IEnumerable<int>? enumerable = null;
            foreach (var _ in enumerable.OmSelect(x => x.ToString()))
            { }
        };

        Assert.Throws<NullReferenceException>(func);
    }

    [Fact]
    public void OmSelect_should_work_well_when_not_null()
    {
        var source = LinqHelper.Range(5).OmSelect(x => x * 2);
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }

    [Fact]
    public void OmSelect_with_index_should_have_got_right_indexes()
    {
        Assert.All(LinqHelper.Range(10).OmSelect((_, i) => i).OmToList(), (x, i) => Assert.Equal(x, i));
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MySelectTest
{
    [Fact]
    public void MySelect_should_not_enumerate_early()
    {
        var spy = new EnumerableSpy();

        var source = spy.GetValues().MySelect(x => x.ToString());
        Assert.False(spy.IsEnumerated);

        _ = source.MyTake(5).MyToList();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public void MySelect_should_enumerate_each_item_once()
    {
        var spy = new EnumerableSpy();

        _ = spy.GetValues().MySelect(x => x.ToString()).MyToList();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public void MySelect_should_not_make_stack_overflow()
    {
        var spy = new EnumerableSpy();

        _ = spy.GetValues().MySelect(x => x.ToString()).MyTake(5).MyToList();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void MySelect_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IEnumerable<int>? enumerable = null;
            _ = enumerable.MySelect(x => x.ToString());
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public void MySelect_should_throw_when_null_on_enumeration()
    {
        var func = () =>
        {
            IEnumerable<int>? enumerable = null;
            foreach (var _ in enumerable.MySelect(x => x.ToString()))
            { }
        };

        Assert.Throws<NullReferenceException>(func);
    }

    [Fact]
    public void MySelect_should_work_well_when_not_null()
    {
        var source = LinqHelper.Range(5).MySelect(x => x * 2);
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }

    [Fact]
    public void MySelect_with_index_should_have_got_right_indexes()
    {
        Assert.All(LinqHelper.Range(10).MySelect((_, i) => i).MyToList(), (x, i) => Assert.Equal(x, i));
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MySelectAsyncTest
{
    [Fact]
    public async Task MySelectAsync_should_not_enumerate_early()
    {
        var spy = new EnumerableSpy();

        var source = spy.GetValues().MySelectAsync(x => TaskHelper.ToTask<string>(x.ToString()));
        Assert.False(spy.IsEnumerated);

        _ = await source.MyTakeAsync(5).MyToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public async Task MySelectAsync_should_enumerate_each_item_once()
    {
        var spy = new EnumerableSpy();

        _ = await spy.GetValues().MySelectAsync(x => x.ToString().ToTask()).MyToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task MySelectAsync_should_not_make_stack_overflow()
    {
        var spy = new EnumerableSpy();

        _ = await spy.GetValues().MySelectAsync(x => x.ToString().ToTask()).MyTakeAsync(5).MyToListAsync();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void MySelectAsync_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IAsyncEnumerable<int>? enumerable = null;
            _ = enumerable.MySelectAsync(x => x.ToString());
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public void MySelectAsync_should_throw_when_null_on_enumeration()
    {
        var func = async () =>
        {
            IEnumerable<int>? enumerable = null;
            await foreach (var _ in enumerable.MySelectAsync(x => x.ToString().ToTask()))
            { }
        };

        Assert.ThrowsAsync<NullReferenceException>(func);
    }

    [Fact]
    public async Task MySelectAsync_should_work_well_when_not_null()
    {
        var source = await LinqHelper.Range(5).MySelectAsync(x => (x * 2).ToTask()).MyToListAsync();
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }
}
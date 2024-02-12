using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmSelectAsyncTest : TestBase
{
    [Fact]
    public async Task OmSelectAsync_should_not_enumerate_early()
    {
        var spy = new SpyEnumerable();

        var source = spy.GetValues().OmSelectAsync(x => x.ToString().ToTask());
        Assert.False(spy.IsEnumerated);

        _ = await source.OmTakeAsync(5).OmToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public async Task OmSelectAsync_should_enumerate_each_item_once()
    {
        var spy = new SpyEnumerable();

        _ = await spy.GetValues().OmSelectAsync(x => x.ToString().ToTask()).OmToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task OmSelectAsync_should_not_make_stack_overflow()
    {
        var spy = new SpyEnumerable();

        _ = await spy.GetValues().OmSelectAsync(x => x.ToString().ToTask()).OmTakeAsync(5).OmToListAsync();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void OmSelectAsync_should_not_throw_when_null_before_enumeration()
    {
        try
        {
            IAsyncEnumerable<int>? enumerable = null;
            _ = enumerable.OmSelectAsync(x => x.ToString());
        }
        catch
        {
            Assert.Fail("Should not got exception");
        }
    }

    [Fact]
    public async Task OmSelectAsync_should_throw_when_null_on_enumeration()
    {
        var func = async () =>
        {
            IEnumerable<int>? enumerable = null;
            await foreach (var _ in enumerable.OmSelectAsync(x => x.ToString().ToTask()))
            { }
        };

        await Assert.ThrowsAsync<NullReferenceException>(func);
    }

    [Fact]
    public async Task OmSelectAsync_should_work_well_when_not_null()
    {
        var source = await LinqHelper.Range(5).OmSelectAsync(x => (x * 2).ToTask()).OmToListAsync();
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }
}
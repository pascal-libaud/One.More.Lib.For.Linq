using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmToListAsyncTest : TestBase
{
    [Fact]
    public async Task OmToListAsync_should_throw_on_null_enumerable()
    {
        var func = async () =>
        {
            IAsyncEnumerable<int>? enumerable = null;
            return await enumerable.OmToListAsync();
        };

        await Assert.ThrowsAsync<NullReferenceException>(func);
    }

    [Fact]
    public async Task OmToList_should_enumerate_each_item()
    {
        var spy = new SpyEnumerable();

        _ = await spy.GetValuesAsync().OmToListAsync();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task OmToListAsync_should_enumerate_each_item_once()
    {
        var spy = new SpyEnumerable();

        _ = await spy.GetValuesAsync().OmToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }
}
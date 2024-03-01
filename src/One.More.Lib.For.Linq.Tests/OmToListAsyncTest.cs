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
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmToListAsync();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public async Task OmToListAsync_should_enumerate_each_item_once()
    {
        var omToListAsync = async (IAsyncEnumerable<int> x) => await x.OmToListAsync();
        await omToListAsync.Should_enumerate_each_item_once();
    }
}
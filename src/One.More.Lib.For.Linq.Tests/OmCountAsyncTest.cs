namespace One.More.Lib.For.Linq.Tests;

public class OmCountAsyncTest : TestBase
{
    [Fact]
    public async Task OmCountAsync_should_work_as_expected()
    {
        Assert.Equal(10, await SpyAsyncEnumerable.GetValuesAsync().OmCountAsync());
    }

    [Fact]
    public async Task OmCountAsync_should_enumerate_only_once()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmCountAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }
}
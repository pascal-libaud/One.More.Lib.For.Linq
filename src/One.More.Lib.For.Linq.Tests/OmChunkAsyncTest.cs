using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmChunkAsyncTest : TestBase
{
    [Fact]
    public async Task OmChunk_should_work_as_expected()
    {
        var spy = new SpyEnumerable();

        var result = await spy.GetValuesAsync().OmChunkAsync(3).OmToListAsync();

        Assert.Equal(4, result.Count);

        Assert.Equal(result[0], new List<int> { 0, 1, 2 });
        Assert.Equal(result[1], new List<int> { 3, 4, 5 });
        Assert.Equal(result[2], new List<int> { 6, 7, 8 });
        Assert.Equal(result[3], new List<int> { 9 });
    }

    [Fact]
    public async Task OmChunk_should_not_enumerate_early()
    {
        var omChunkAsync = (IAsyncEnumerable<int> x) => x.OmChunkAsync(3);
        await omChunkAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task OmChunk_should_enumerate_each_item_once()
    {
        var spy = new SpyEnumerable();

        _ = await spy.GetValuesAsync().OmChunkAsync(3).OmToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }
}
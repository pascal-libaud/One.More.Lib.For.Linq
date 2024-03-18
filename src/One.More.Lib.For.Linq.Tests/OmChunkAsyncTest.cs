namespace One.More.Lib.For.Linq.Tests;

public class OmChunkAsyncTest : TestBase
{
    [Fact]
    public async Task OmChunkAsync_should_work_as_expected()
    {
        var result = await LinqAsyncHelper.RangeAsync(10).OmChunkAsync(3).OmToListAsync();

        Assert.Equal(4, result.Count);

        Assert.Equal(result[0], new List<int> { 0, 1, 2 });
        Assert.Equal(result[1], new List<int> { 3, 4, 5 });
        Assert.Equal(result[2], new List<int> { 6, 7, 8 });
        Assert.Equal(result[3], new List<int> { 9 });
    }

    [Fact]
    public async Task OmChunkAsync_should_work_with_empty_source()
    {
        Assert.Equal(0, await LinqAsyncHelper.EmptyAsync<int>().OmChunkAsync(3).OmCountAsync());
    }

    [Fact]
    public async Task OmChunkAsync_should_not_return_last_empty_chunk()
    {
        Assert.Equal(3, await LinqAsyncHelper.RangeAsync(9).OmChunkAsync(3).OmCountAsync());
    }

    [Fact]
    public async Task OmChunkAsync_should_not_enumerate_early()
    {
        var omChunkAsync = (IAsyncEnumerable<int> x) => x.OmChunkAsync(3);
        await omChunkAsync.Should_not_enumerate_early();
    }

    [Fact]
    public async Task OmChunkAsync_should_enumerate_each_item_once()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmChunkAsync(3).OmToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task OmChunkAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmChunkAsync(3, token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmChunkAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmChunkAsync(3).OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

}
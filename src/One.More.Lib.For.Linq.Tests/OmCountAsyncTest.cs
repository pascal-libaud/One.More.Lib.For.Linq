namespace One.More.Lib.For.Linq.Tests;

public class OmCountAsyncTest : TestBase
{
    [Fact]
    public async Task OmCountAsync_should_work_as_expected()
    {
        Assert.Equal(10, await LinqAsyncHelper.RangeAsync(10).OmCountAsync());
    }

    [Fact]
    public async Task OmCountAsync_should_enumerate_only_once()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmCountAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task OmDistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmCountAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmDistinctAsync_should_pass_cancellation_token_bis()
    { 
        IAsyncEnumerable<int> GetValues()
        {
            return LinqAsyncHelper.RangeAsync(10);
        }

        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await GetValues().OmCountAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
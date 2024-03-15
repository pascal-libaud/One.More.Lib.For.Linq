namespace One.More.Lib.For.Linq.Tests;

public class InfiniteIteratorAsyncTest : TestBase
{
    [Fact]
    public async Task InfiniteIteratorAsync_should_not_make_infinite_loop()
    {
        var iterator = Task.Run(() => LinqAsyncHelper.InfiniteIteratorAsync<int>().OmTakeAsync(4).OmToListAsync());

        var delay = Task.Delay(1000);
        var result = await Task.WhenAny(iterator, delay);

        Assert.Equal(result, iterator);
    }

    [Fact]
    public async Task InfiniteIteratorAsync_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.InfiniteIteratorAsync<int>(token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task InfiniteIteratorAsync_should_receive_cancellation_token_and_cancel()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.InfiniteIteratorAsync<int>().OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
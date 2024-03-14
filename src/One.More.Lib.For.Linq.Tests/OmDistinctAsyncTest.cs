namespace One.More.Lib.For.Linq.Tests;

public class OmDistinctAsyncTest : TestBase
{
    private static SpyAsyncEnumerable<int> GetSpy()
    {
        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        return SpyAsyncEnumerable.GetValuesAsync(source);
    }

    [Fact]
    public async Task OmDistinctAsync_should_work_as_expected()
    {
        var spy = GetSpy();

        var actual = await spy.OmDistinctAsync().OmToListAsync();
        actual.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
    }

    [Fact]
    public async Task OmDistinctAsync_should_enumerate_each_item_once()
    {
        var spy = GetSpy();

        _ = await spy.OmDistinctAsync().OmToListAsync();
        Assert.Equal(1, spy.CountEnumeration);
    }

    [Fact]
    public async Task OmDistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var func = async () => await source.OmDistinctAsync(token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact] 
    public async Task OmDistinctAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 }.ToAsyncEnumerable();
        var func = async () => await source.OmDistinctAsync().OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
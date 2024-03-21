namespace One.More.Lib.For.Linq.Tests;

public class OmHasDuplicateAsyncTest : TestBase
{
    [Fact]
    public async Task OmHasDuplicateAsync_should_work_as_expected()
    {
        Assert.False(await LinqAsyncHelper.EmptyAsync<int>().OmHasDuplicateAsync());

        var source = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Assert.False(await source.ToAsyncEnumerable().OmHasDuplicateAsync());

        source.Add(3);
        Assert.True(await source.ToAsyncEnumerable().OmHasDuplicateAsync());
    }

    [Fact]
    public async Task OmHasDuplicateAsync_should_enumerate_each_item_once()
    {
        var hasDuplicateAsync = (IAsyncEnumerable<int> x) => x.OmHasDuplicateAsync();
        await hasDuplicateAsync.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task OmHasDuplicateAsync_should_not_enumerate_all_when_found_duplicate()
    {
        var spy = new SpyAsyncEnumerable<int>(new List<int> { 0, 1, 2, 2, 3, 4 }.ToAsyncEnumerable());
        var hasDuplicateAsync = (IAsyncEnumerable<int> x) => x.OmHasDuplicateAsync();
        await hasDuplicateAsync.Should_enumerate_each_item_once(spy);
    }

    [Fact]
    public async Task OmHasDuplicateAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await SpyAsyncEnumerable.GetValuesAsync().OmHasDuplicateAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
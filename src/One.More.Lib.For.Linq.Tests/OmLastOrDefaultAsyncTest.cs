namespace One.More.Lib.For.Linq.Tests;

public class OmLastOrDefaultAsyncAsyncTest : TestBase
{
    [Fact]
    public async Task OmLastOrDefaultAsync_should_work_as_expected()
    {
        var result = await LinqAsyncHelper.RangeAsync(0, 10).OmLastOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmLastOrDefaultAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmLastOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmLastOrDefaultAsync();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmLastOrDefaultAsync(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.EmptyAsync<int?>().OmLastOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeAsync(5, 5).OmLastOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeNullableAsync(0, 10).OmLastOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int?>().OmLastOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeAsync(0, 10).OmLastOrDefaultAsync(x => x == 20);
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmLastOrDefaultAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmLastOrDefaultAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

}
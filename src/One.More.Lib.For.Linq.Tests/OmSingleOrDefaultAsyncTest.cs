namespace One.More.Lib.For.Linq.Tests;

public class OmSingleOrDefaultAsyncTest : TestBase
{
    [Fact]
    public async Task OmSingleOrDefaultAsync_should_work_as_expected()
    {
        var result = await LinqAsyncHelper.RangeAsync(0, 10).OmSingleOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_should_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmSingleOrDefaultAsync(x => x == 2);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one matching element");

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmSingleOrDefaultAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmSingleOrDefaultAsync(x => x == 0);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.EmptyAsync<int?>().OmSingleOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeAsync(5, 5).OmSingleOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeNullableAsync(0, 10).OmSingleOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int?>().OmSingleOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmSingleOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeNullableAsync(0, 10).OmSingleOrDefaultAsync(x => x == 20);
        await func.Should().NotThrowAsync();
    }
}
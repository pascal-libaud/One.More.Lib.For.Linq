namespace One.More.Lib.For.Linq.Tests;

public class OmSingleAsyncTest : TestBase
{
    [Fact]
    public async Task OmSingleAsync_should_work_as_expected()
    {
        var result = await LinqAsyncHelper.RangeAsync(0, 10).OmSingleAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task OmSingleAsync_should_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmSingleAsync(x => x == 2);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one matching element");

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmSingleAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    [Fact]
    public void OmSingleAsync_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = spy.OmSingleAsync(x => x == 0);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmSingleAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int>().OmSingleAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task OmSingleAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeNullableAsync(0, 10).OmSingleAsync(x => x == 20);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }
}
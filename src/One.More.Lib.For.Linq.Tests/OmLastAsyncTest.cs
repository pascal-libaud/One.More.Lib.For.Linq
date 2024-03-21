namespace One.More.Lib.For.Linq.Tests;

public class OmLastAsyncTest : TestBase
{
    [Fact]
    public async Task OmLastAsync_should_work_as_expected()
    {
        DummyIndexValue[] source =
        [
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x, x)),
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x + 10, x))
        ];

        var result = await source.ToAsyncEnumerable().OmLastAsync(x => x.Value == 5);
        Assert.Equal(new DummyIndexValue(15, 5), result);
    }

    [Fact]
    public async Task OmLastAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmLastAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmLastAsync();
        await func.Should().NotThrowAsync();
    }

    // TODO Voir comment en faire une méthode d'extension
    [Fact]
    public async Task OmLastAsync_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await spy.OmLastAsync();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmLastAsync_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();
        
        _ = await spy.OmLastAsync(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public async Task OmLastAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int>().OmLastAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task OmLastAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeNullableAsync(0, 10).OmLastAsync(x => x == 20);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task OmLastAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmLastAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
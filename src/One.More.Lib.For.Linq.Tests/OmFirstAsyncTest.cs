namespace One.More.Lib.For.Linq.Tests;

public class OmFirstAsyncTest : TestBase
{
    [Fact]
    public async Task OmFirstAsync_should_work_as_expected()
    {
        DummyIndexValue[] source =
        [
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x, x)),
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x + 10, x))
        ];

        var result = await source.ToAsyncEnumerable().OmFirstAsync(x => x.Value == 5);
        Assert.Equal(new DummyIndexValue(5, 5), result);
    }

    [Fact]
    public async Task OmFirstAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmFirstAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmFirstAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmFirstAsync_without_predicate_should_not_enumerate_all()
    {
        var omFirstAsync = (IAsyncEnumerable<int> x) => x.OmFirstAsync();
        await omFirstAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmFirstAsync_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirstAsync = (IAsyncEnumerable<int> x) => x.OmFirstAsync(z => z == 5);
        await omFirstAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmFirstAsync_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int>().OmFirstAsync();
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public async Task OmFirstAsync_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeNullableAsync(0, 10).OmFirstAsync(x => x == 20);
        (await func.Should().ThrowAsync<InvalidOperationException>())
            .And.Message.Should().Be("Sequence contains no matching element");
    }

    [Fact]
    public async Task OmFirstAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmFirstAsync(x => x == 2, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
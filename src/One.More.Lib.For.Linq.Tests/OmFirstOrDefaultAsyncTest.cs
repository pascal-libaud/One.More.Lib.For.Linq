namespace One.More.Lib.For.Linq.Tests;

public class OmFirstOrDefaultAsyncAsyncTest : TestBase
{
    [Fact]
    public async Task OmFirstOrDefaultAsync_should_work_as_expected()
    {
        var result = await LinqAsyncHelper.RangeAsync(0, 10).OmFirstOrDefaultAsync(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.ToAsyncEnumerable().OmFirstOrDefaultAsync(x => x == 2);
        await func.Should().NotThrowAsync();

        func = () => new List<int> { 1, 2 }.ToAsyncEnumerable().OmFirstOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_without_predicate_should_not_enumerate_all()
    {
        var omFirstOrDefaultAsync = (IAsyncEnumerable<int> x) => x.OmFirstOrDefaultAsync();
        await omFirstOrDefaultAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirstOrDefaultAsync = (IAsyncEnumerable<int> x) => x.OmFirstOrDefaultAsync(z => z == 5);
        await omFirstOrDefaultAsync.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_without_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.EmptyAsync<int?>().OmFirstOrDefaultAsync();
        Assert.Null(result);
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeAsync(5, 5).OmFirstOrDefaultAsync(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_with_predicate_should_return_default_when_no_item_found()
    {
        var result = await LinqAsyncHelper.RangeNullableAsync(0, 10).OmFirstOrDefaultAsync(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.EmptyAsync<int?>().OmFirstOrDefaultAsync();
        await func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task OmFirstOrDefaultAsync_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqAsyncHelper.RangeAsync(0, 10).OmFirstOrDefaultAsync(x => x == 20);
        await func.Should().NotThrowAsync();
    }
}
namespace One.More.Lib.For.Linq.Tests;

public class OmOfTypeAsyncTest : TestBase
{
    [Fact]
    public async Task OmOfTypeAsync_should_return_only_matching_elements()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var result = await source.OmOfTypeAsync<DummyBase, Dummy1>().OmToListAsync();

        result.Should().BeEquivalentTo(new List<Dummy1> { new(), new() }, x => x.ComparingRecordsByValue());
    }

    [Fact]
    public async Task OmOfTypeAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmOfTypeAsync<int, int>().OmToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task OmOfTypeAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmOfTypeAsync<int, int>();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task OmOfTypeAsync_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmOfTypeAsync<int, int>().OmTakeAsync(2).OmToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmOfTypeAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var func = async () => await source.OmOfTypeAsync<DummyBase, Dummy1>(token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmOfTypeAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var func = async () => await source.OmOfTypeAsync<DummyBase, Dummy1>().OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
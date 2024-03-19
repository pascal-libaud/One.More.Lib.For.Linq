namespace One.More.Lib.For.Linq.Tests;

public class OmCastAsyncTest : TestBase
{
    [Fact]
    public async Task OmCastAsync_should_return_as_many_elements_as_the_source_list()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy1(), new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var result = await source.OmCastAsync<DummyBase, Dummy1>().OmToListAsync();
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task OmCastAsync_should_throw_an_exception_when_cast_is_invalid()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() }.ToAsyncEnumerable();
        var action = () =>  source.OmCastAsync<DummyBase, Dummy1>().OmToListAsync();
        await action.Should().ThrowAsync<InvalidCastException>();
    }

    [Fact]
    public async Task OmCastAsync_should_enumerate_each_item_once()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmCastAsync<int, int>().OmToListAsync();
        await sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public async Task OmCastAsync_should_not_enumerate_early()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmCastAsync<int, int>();
        await sut.Should_not_enumerate_early();
    }

    [Fact]
    public async Task OmCastAsync_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IAsyncEnumerable<int> x) => x.OmCastAsync<int, int>().OmTakeAsync(2).OmToListAsync();
        await sut.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmCastAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var func = async () => await source.OmCastAsync<DummyBase, Dummy1>(token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmCastAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = new List<DummyBase> { new Dummy1(), new Dummy1() }.ToAsyncEnumerable();
        var func = async () => await source.OmCastAsync<DummyBase, Dummy1>().OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
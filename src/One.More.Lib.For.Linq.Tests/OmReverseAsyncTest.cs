namespace One.More.Lib.For.Linq.Tests;

public class OmReverseAsyncTest : TestBase
{
    [Fact]
    public async Task OmReverseAsync_should_reverse_alphabet()
    {
        var expected = new[] { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N',
            'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A' };
        Assert.Equal(expected, await LinqHelper.Alphabet().ToAsyncEnumerable().OmReverseAsync().OmToArrayAsync());
    }

        [Fact]
    public async Task OmDistinctAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = LinqHelper.Alphabet().ToAsyncEnumerable();
        var func = async () => await source.OmReverseAsync(token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
    [Fact] 
    public async Task OmDistinctAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = LinqHelper.Alphabet().ToAsyncEnumerable();
        var func = async () => await source.OmReverseAsync().OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

}
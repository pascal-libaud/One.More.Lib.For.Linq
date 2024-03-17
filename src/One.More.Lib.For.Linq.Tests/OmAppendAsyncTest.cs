using System.Runtime.CompilerServices;

namespace One.More.Lib.For.Linq.Tests;

public class OmAppendAsyncTest : TestBase
{
    static async IAsyncEnumerable<string> GetValuesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Yield();
        yield return "P";
        yield return "Q";
    }

    [Fact]
    public async Task OmAppendAsync_should_add_in_end_when_source_not_null()
    {

        Assert.Equal(new[] { "P", "Q", "R" }, await GetValuesAsync().OmAppendAsync("R").OmToListAsync());
    }

    [Fact]
    public async Task OmAppendAsync_should_work_well_when_source_empty()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield break;
        }

        Assert.Equal(new[] { "R" }, await GetValuesAsync().OmAppendAsync("R").OmToListAsync());
    }

    [Fact]
    public async Task OmAppendAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var func = async () => await source.OmAppendAsync("R", token.Token).OmToListAsync();
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmAppendAsync_should_receive_and_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var source = GetValuesAsync();
        var func = async () => await source.OmAppendAsync("R").OmToListAsync(token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}
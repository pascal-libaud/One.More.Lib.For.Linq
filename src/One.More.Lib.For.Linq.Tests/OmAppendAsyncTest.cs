namespace One.More.Lib.For.Linq.Tests;

public class OmAppendAsyncTest : TestBase
{
    [Fact]
    public async Task OmAppendAsync_should_add_in_end_when_source_not_null()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield return "P";
            yield return "Q";
        }

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
}
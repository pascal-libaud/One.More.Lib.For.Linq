namespace One.More.Lib.For.Linq.Tests;

public class OmPrependAsyncTest : TestBase
{
    [Fact]
    public async Task OmPrependAsync_should_add_in_end_when_source_not_null()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield return "P";
            yield return "Q";
        }

        Assert.Equal(new[] { "O", "P", "Q" }, await GetValuesAsync().OmPrependAsync("O").OmToListAsync());
    }

    [Fact]
    public async Task OmPrependAsync_should_work_well_when_source_empty()
    {
        static async IAsyncEnumerable<string> GetValuesAsync()
        {
            await Task.Yield();
            yield break;
        }

        Assert.Equal(new[] { "R" }, await GetValuesAsync().OmPrependAsync("R").OmToListAsync());
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class EmptyAsyncTest : TestBase
{
    [Fact]
    public void EmptyAsync_should_not_be_null()
    {
        var result = LinqHelper.EmptyAsync<int>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task EmptyAsync_should_be_empty()
    {
        var result = await LinqHelper.EmptyAsync<int>().OmToListAsync();
        Assert.Empty(result);
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class EmptyTest
{
    [Fact]
    public void Empty_should_not_be_null()
    {
        var result = LinqHelper.Empty<int>();
        Assert.NotNull(result);
    }

    [Fact]
    public void Empty_should_be_empty()
    {
        var result = LinqHelper.Empty<int>().OmToList();
        Assert.Empty(result);
    }
}
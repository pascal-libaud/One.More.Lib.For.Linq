namespace One.More.Lib.For.Linq.Tests;

public class OmAnyTest : TestBase
{
    [Fact]
    public void OmAny_should_return_false_when_no_item_matches()
    {
        Assert.False(LinqHelper.Range(0, 10).OmAny(x => x > 10));
    }

    [Fact]
    public void OmAny_should_return_true_when_at_least_one_item_matches()
    {
        Assert.True(LinqHelper.Range(0, 10).OmAny(x => x  == 2));
    }

    [Fact]
    public void OmAny_should_return_false_when_empty()
    {
        Assert.False(LinqHelper.Empty<int>().OmAny(x => x == 10));
    }

    [Fact]
    public void OmAny_should_not_enumerate_all_when_one_true()
    {
        var OmAny = (IEnumerable<int> x) => x.OmAny(z => z == 8);
        OmAny.Should_not_enumerate_all_when();
    }
}
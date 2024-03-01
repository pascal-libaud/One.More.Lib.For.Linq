namespace One.More.Lib.For.Linq.Tests;

public class OmAllTest : TestBase
{
    [Fact]
    public void OmAll_should_return_true_when_empty()
    {
        Assert.True(LinqHelper.Empty<int>().OmAll(x => x == 10));
    }

    [Fact]
    public void OmAll_should_not_enumerate_all_when_one_false()
    {
        var omAll = (IEnumerable<int> x) => x.OmAll(z => z < 8);
        omAll.Should_not_enumerate_all_when();
    }
}
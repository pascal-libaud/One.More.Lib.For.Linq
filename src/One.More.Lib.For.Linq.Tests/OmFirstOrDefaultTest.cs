using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmFirstOrDefaultTest : TestBase
{
    [Fact]
    public void OmFirstOrDefault_without_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirstOrDefault = (IEnumerable<int> x) => x.OmFirstOrDefault();
        omFirstOrDefault.Should_not_enumerate_all_when();
    }

    [Fact]
    public void OmFirstOrDefault_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirstOrDefault = (IEnumerable<int> x) => x.OmFirstOrDefault(z => z == 5);
        omFirstOrDefault.Should_not_enumerate_all_when();
    }

    [Fact]
    public void OmFirstOrDefault_without_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.Empty<int?>().OmFirstOrDefault();
        Assert.Null(result);
    }

    [Fact]
    public void OmFirstOrDefault_with_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.RangeNullable(0, 10).OmFirstOrDefault(x => x == 20);
        Assert.Null(result);
    }
}
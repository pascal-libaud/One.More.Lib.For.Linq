namespace One.More.Lib.For.Linq.Tests;

public class OmFirstOrDefaultTest : TestBase
{
    [Fact]
    public void OmFirstOrDefault_should_work_as_expected()
    {
        var result = LinqHelper.Range(0, 10).OmFirstOrDefault(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void OmFirstOrDefault_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.OmFirstOrDefault(x => x == 2);
        func.Should().NotThrow();

        func = () => new List<int> { 1, 2 }.OmFirstOrDefault();
        func.Should().NotThrow();
    }

    [Fact]
    public void OmFirstOrDefault_without_predicate_should_not_enumerate_all()
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
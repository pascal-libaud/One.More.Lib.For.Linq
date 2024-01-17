using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmFirstOrDefaultTest
{
    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmFirstOrDefault_without_predicate_should_not_enumerate_all_when_item_found(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        var spy = new EnumerableSpy();
        _ = spy.GetValues().OmFirstOrDefault();
        Assert.Equal(1, spy.CountItemEnumerated);
    }

    [Fact]
    internal void OmFirstOrDefault_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var spy = new EnumerableSpy();
        _ = spy.GetValues().OmFirstOrDefault(x => x == 5);
        Assert.Equal(6, spy.CountItemEnumerated);
    }

    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmFirstOrDefault_without_predicate_should_return_default_when_no_item_found(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        var result = LinqHelper.Empty<int?>().OmFirstOrDefault();
        Assert.Null(result);
    }

    [Fact]
    internal void OmFirstOrDefault_with_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.RangeNullable(0, 10).OmFirstOrDefault(x => x == 20);
        Assert.Null(result);
    }
}
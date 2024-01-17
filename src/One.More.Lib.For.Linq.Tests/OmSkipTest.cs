using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmSkipTest
{
    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmSkip_should_return_empty_when_count_too_big(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        Assert.Empty(LinqHelper.Range(10).OmSkip(10));
        Assert.Empty(LinqHelper.Range(10).OmSkip(12));
    }

    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmSkip_should_return_only_one_item_when_count_equals_count_minus_one(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        Assert.Equal(LinqHelper.Range(10).OmSkip(9), new[] { 9 });
    }

    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmSkip_should_not_reorder_items(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        Assert.Equal(LinqHelper.Range(10).OmSkip(0), LinqHelper.Range(10));
    }

    [Theory]
    [InlineData(EnumerationWay.Foreach)]
    [InlineData(EnumerationWay.Enumerator)]
    internal void OmSkip_should_not_change_anything_when_count_equals_zero(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        Assert.Equal(LinqHelper.Range(10).OmSkip(0), LinqHelper.Range(10));
    }
}
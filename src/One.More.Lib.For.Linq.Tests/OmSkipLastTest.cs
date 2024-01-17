using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmSkipLastTest
{
    [Fact]
    internal void OmSkipLast_should_return_empty_when_count_too_big()
    {
        Assert.Empty(LinqHelper.Range(10).OmSkipLast(10));
        Assert.Empty(LinqHelper.Range(10).OmSkipLast(12));
    }

    [Fact]
    internal void OmSkipLast_should_return_only_one_item_when_count_equals_count_minus_one()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkipLast(9), new[] { 0 });
    }

    [Fact]
    internal void OmSkipLast_should_not_reorder_items()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkipLast(0), LinqHelper.Range(10));
    }

    [Fact]
    internal void OmSkipLast_should_not_change_anything_when_count_equals_zero()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkipLast(0), LinqHelper.Range(10));
    }
}
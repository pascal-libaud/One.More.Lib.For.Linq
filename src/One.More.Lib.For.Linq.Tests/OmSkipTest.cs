namespace One.More.Lib.For.Linq.Tests;

public class OmSkipTest : TestBase
{
    [Fact]
    public void OmSkip_should_return_empty_when_count_too_big()
    {
        Assert.Empty(LinqHelper.Range(10).OmSkip(10));
        Assert.Empty(LinqHelper.Range(10).OmSkip(12));
    }

    [Fact]
    public void OmSkip_should_return_only_one_item_when_count_equals_count_minus_one()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkip(9), [9]);
        Assert.Equal(LinqHelper.Range(10).OmToList().OmSkip(9), [9]);
    }

    [Fact]
    public void OmSkip_should_not_reorder_items()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkip(0), LinqHelper.Range(10));
    }

    [Fact]
    public void OmSkip_should_not_change_anything_when_count_equals_zero()
    {
        Assert.Equal(LinqHelper.Range(10).OmSkip(0), LinqHelper.Range(10));
    }
}
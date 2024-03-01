namespace One.More.Lib.For.Linq.Tests;

public class OmSingleOrDefaultTest : TestBase
{
    [Fact]
    public void OmSingleOrDefault_should_work_as_expected()
    {
        var result = LinqHelper.Range(0, 10).OmSingleOrDefault(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void OmSingleOrDefault_should_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.OmSingleOrDefault(x => x == 2);
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains more than one matching element");

        func = () => new List<int> { 1, 2 }.OmSingleOrDefault();
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    // TODO le déplacer dans TestHelper et le tester sur plus de méthodes
    [Fact]
    public void OmSingleOrDefault_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmSingleOrDefault(x => x == 1);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmSingleOrDefault_without_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.Empty<int?>().OmSingleOrDefault();
        Assert.Null(result);
    }

    [Fact]
    public void OmSingleOrDefault_with_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.RangeNullable(0, 10).OmSingleOrDefault(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public void OmSingleOrDefault_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqHelper.Empty<int?>().OmSingleOrDefault();
        func.Should().NotThrow();
    }

    [Fact]
    public void OmSingleOrDefault_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqHelper.RangeNullable(0, 10).OmSingleOrDefault(x => x == 20);
        func.Should().NotThrow();
    }
}
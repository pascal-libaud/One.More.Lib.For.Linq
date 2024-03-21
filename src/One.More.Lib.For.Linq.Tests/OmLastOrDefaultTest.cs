namespace One.More.Lib.For.Linq.Tests;

public class OmLastOrDefaultTest : TestBase
{
    [Fact]
    public void OmLastOrDefault_should_work_as_expected()
    {
        var result = LinqHelper.Range(0, 10).OmLastOrDefault(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void OmLastOrDefault_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.OmLastOrDefault(x => x == 2);
        func.Should().NotThrow();

        func = () => new List<int> { 1, 2 }.OmLastOrDefault();
        func.Should().NotThrow();
    }

    // TODO Voir comment en faire une méthode d'extension
    [Fact]
    public void OmLastOrDefault_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmLastOrDefault();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmLastOrDefault_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmLastOrDefault(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmLastOrDefault_without_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.Empty<int?>().OmLastOrDefault();
        Assert.Null(result);
    }

    [Fact]
    public void OmLastOrDefault_with_predicate_should_return_zero_when_no_item_found()
    {
        var result = LinqHelper.Range(5, 5).OmLastOrDefault(x => x == 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public void OmLastOrDefault_with_predicate_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.RangeNullable(0, 10).OmLastOrDefault(x => x == 20);
        Assert.Null(result);
    }

    [Fact]
    public void OmLastOrDefault_without_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqHelper.Empty<int?>().OmLastOrDefault();
        func.Should().NotThrow();
    }

    [Fact]
    public void OmLastOrDefault_with_predicate_should_not_throw_when_no_item_found()
    {
        var func = () => LinqHelper.RangeNullable(0, 10).OmLastOrDefault(x => x == 20);
        func.Should().NotThrow();
    }
}
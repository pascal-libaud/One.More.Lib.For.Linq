namespace One.More.Lib.For.Linq.Tests;

public class OmLastTest : TestBase
{
    [Fact]
    public void OmLast_should_work_as_expected()
    {
        DummyIndexValue[] source =
        [
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x, x)),
            .. LinqHelper.Range(0, 10).OmSelect(x => new DummyIndexValue(x + 10, x))
        ];

        var result = source.OmLast(x => x.Value == 5);
        Assert.Equal(new DummyIndexValue(15, 5), result);
    }

    [Fact]
    public void OmLast_should_not_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.OmLast(x => x == 2);
        func.Should().NotThrow();

        func = () => new List<int> { 1, 2 }.OmLast();
        func.Should().NotThrow();
    }

    [Fact]
    public void OmLast_without_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmLast();

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmLast_with_predicate_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmLast(x => x == 5);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmLast_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.Empty<int>().OmLast();
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public void OmLast_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.RangeNullable(0, 10).OmLast(x => x == 20);
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no matching element");
    }
}
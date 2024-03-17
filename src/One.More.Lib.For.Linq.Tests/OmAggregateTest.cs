namespace One.More.Lib.For.Linq.Tests;

public class OmAggregateTest : TestBase
{
    [Fact]
    public void OmAggregate_should_work_as_expected()
    {
        var source = LinqHelper.Range(10);

        source.OmAggregate((x, y) => x + y).Should().Be(45);
    }

    [Fact]
    public void OmAggregate_with_seed_should_work_as_expected()
    {
        var source = LinqHelper.Range(10);

        source.OmAggregate(10, (x, y) => x + y).Should().Be(55);
    }

    [Fact]
    public void OmAggregate_with_seed_and_result_selector_should_work_as_expected()
    {
        var source = LinqHelper.Range(10);

        source.OmAggregate(10, (x, y) => x + y, x => x * 2).Should().Be(110);
    }

    [Fact]
    public void OmAggregate_should_throw_exception_on_empty_source()
    {
        var source = LinqHelper.Empty<int>();

        var func = () => source.OmAggregate((x, y) => x + y);

        func.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void OmAggregate_with_seed_should_not_throw_exception_on_empty_source()
    {
        var source = LinqHelper.Empty<int>();

        var func = () => source.OmAggregate(10, (x, y) => x + y);

        func.Should().NotThrow();
    }

    [Fact]
    public void OmAggregate_with_seed_and_result_selector_should_not_throw_exception_on_empty_source()
    {
        var source = LinqHelper.Empty<int>();

        var func = () => source.OmAggregate(10, (x, y) => x + y);

        func.Should().NotThrow();
    }
}
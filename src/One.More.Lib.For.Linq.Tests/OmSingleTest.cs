namespace One.More.Lib.For.Linq.Tests;

public class OmSingleTest : TestBase
{
    [Fact]
    public void OmSingle_should_work_as_expected()
    {
        var result = LinqHelper.Range(0, 10).OmSingle(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void OmSingle_should_throw_when_found_multiple_candidates()
    {
        var func = () => new List<int> { 0, 1, 2, 2, 3 }.OmSingle(x => x == 2);
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains more than one matching element");

        func = () => new List<int> { 1, 2 }.OmSingle();
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains more than one element");
    }

    // TODO le déplacer dans TestHelper et le tester sur plus de méthodes
    // Voir pour le faire aussi avec la version async
    [Fact]
    public void OmSingle_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmSingle(x => x == 0);

        Assert.True(spy.IsEndReached);
    }

    [Fact]
    public void OmSingle_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.Empty<int>().OmSingle();
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public void OmSingle_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.RangeNullable(0, 10).OmSingle(x => x == 20);
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no matching element");
    }
}
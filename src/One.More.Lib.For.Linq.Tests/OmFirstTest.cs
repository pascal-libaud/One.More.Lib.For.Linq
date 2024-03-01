using FluentAssertions;
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmFirstTest : TestBase
{
    [Fact]
    public void OmFirst_should_work_as_expected()
    {
        var result = LinqHelper.Range(0, 10).OmFirst(x => x == 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void OmFirst_should_not_throw_when_found_multiple_candidates()
    {
        try
        {
            var result = new List<int> { 0, 1, 2, 2, 3 }.OmFirst(x => x == 2);
            Assert.Equal(2, result);

            result = new List<int> { 1, 2 }.OmFirst();
            Assert.Equal(1, result);
        }
        catch (Exception)
        {
            Assert.Fail();
        }
    }

    [Fact]
    public void OmFirst_without_predicate_should_not_enumerate_all()
    {
        var omFirst = (IEnumerable<int> x) => x.OmFirst();
        omFirst.Should_not_enumerate_all_when();
    }

    [Fact]
    public void OmFirst_with_predicate_should_not_enumerate_all_when_item_found()
    {
        var omFirst = (IEnumerable<int> x) => x.OmFirst(z => z == 5);
        omFirst.Should_not_enumerate_all_when();
    }

    [Fact]
    public void OmFirst_without_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.Empty<int>().OmFirst();
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public void OmFirst_with_predicate_should_throw_exception_when_no_item_found()
    {
        var func = () => LinqHelper.RangeNullable(0, 10).OmFirst(x => x == 20);
        func.Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Sequence contains no matching element");
    }
}
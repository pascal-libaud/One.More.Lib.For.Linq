using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmHasDuplicateTest : TestBase
{
    [Fact]
    public void OmHasDuplicate_should_work_as_expected()
    {
        Assert.False(LinqHelper.Empty<int>().OmHasDuplicate());

        var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Assert.False(list.OmHasDuplicate());

        list.Add(3);
        Assert.True(list.OmHasDuplicate());
    }

    [Fact]
    public void OmHasDuplicate_should_enumerate_each_item_once()
    {
        var hasDuplicate = (IEnumerable<int> x) => x.OmHasDuplicate();
        hasDuplicate.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmHasDuplicate_should_not_enumerate_all_when_found_duplicate()
    {
        var spy = new SpyReadOnlyList<int>(new List<int> { 0, 1, 2, 2, 3, 4 });
        var hasDuplicate = (IEnumerable<int> x) => x.OmHasDuplicate();
        hasDuplicate.Should_enumerate_each_item_once(spy);
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class AsEnumerableTest
{
    [Fact]
    public void AsEnumerable_should_return_a_list_containing_only_the_item_passed()
    {
        const int value = 10;
        var result = value.AsEnumerable();
        Assert.Single(result, value);
    }
}
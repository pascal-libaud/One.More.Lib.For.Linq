namespace One.More.Lib.For.Linq.Tests;

public class ToEnumerableTest : TestBase
{
    [Fact]
    public void ToEnumerable_should_return_a_list_containing_only_the_item_passed()
    {
        const int value = 10;
        var result = value.ToEnumerable();
        Assert.Single(result, value);
    }
}
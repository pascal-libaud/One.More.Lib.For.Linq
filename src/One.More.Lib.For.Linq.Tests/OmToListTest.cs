using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmToListTest : TestBase
{
    [Fact]
    public void OmToList_should_throw_on_null_enumerable()
    {
        var func = () =>
        {
            IEnumerable<int>? enumerable = null;
            return enumerable.OmToList();
        };

        Assert.Throws<ArgumentNullException>(func);
    }

    [Fact]
    public void OmToList_should_enumerate_each_item_one()
    {
        var spy = new EnumerableSpy();

        _ = spy.GetValues().OmToList();
        Assert.Equal(1, spy.CountEnumeration);
    }
}
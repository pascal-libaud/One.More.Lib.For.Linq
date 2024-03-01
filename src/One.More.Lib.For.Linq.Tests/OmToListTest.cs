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
    public void OmToList_should_enumerate_each_item()
    {
        var spy = SpyEnumerable.GetValues();

        _ = spy.OmToList();
        Assert.Equal(10, spy.CountItemEnumerated);
    }

    [Fact]
    public void OmToList_should_enumerate_each_item_once()
    {
        var omToList = (IEnumerable<int> x) => x.OmToList();
        omToList.Should_enumerate_each_item_once();
    }
}
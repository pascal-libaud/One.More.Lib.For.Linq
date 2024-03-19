namespace One.More.Lib.For.Linq.Tests;

public class OmCastTest : TestBase
{
    [Fact]
    public void OmCast_should_return_as_many_elements_as_the_source_list()
    {
        var list = new List<DummyBase> { new Dummy1(), new Dummy1(), new Dummy1(), new Dummy1() };
        var result = list.OmCast<Dummy1>().OmToList();
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void OmCast_should_throw_an_exception_when_cast_is_invalid()
    {
        var list = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() };
        var action = () => list.OmCast<Dummy1>().OmToList();
        Assert.Throws<InvalidCastException>(() => action());
    }

    [Fact]
    public void OmCast_should_enumerate_each_item_once()
    {
        var sut = (IEnumerable<int> x) => x.OmCast<int>().OmToList();
        sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmCast_should_not_enumerate_early()
    {
        var sut = (IEnumerable<int> x) => x.OmCast<int>();
        sut.Should_not_enumerate_early();
    }

    [Fact]
    public void OmCast_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IEnumerable<int> x) => x.OmCast<int>().OmTake(2);
        sut.Should_not_enumerate_all_when();
    }
}
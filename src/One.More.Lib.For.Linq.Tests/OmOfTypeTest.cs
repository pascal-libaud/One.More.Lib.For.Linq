namespace One.More.Lib.For.Linq.Tests;

public class OmOfTypeTest : TestBase
{
    [Fact]
    public void OmOfType_should_return_only_matching_elements()
    {
        var source = new List<DummyBase> { new Dummy1(), new Dummy2(), new Dummy1(), new Dummy2() };
        var result = source.OmOfType<Dummy1>();

        result.Should().BeEquivalentTo(new List<Dummy1> { new(), new() }, x => x.ComparingRecordsByValue());
    }

    [Fact]
    public void OmOfType_should_enumerate_each_item_once()
    {
        var sut = (IEnumerable<int> x) => x.OmOfType<int>().OmToList();
        sut.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmOfType_should_not_enumerate_early()
    {
        var sut = (IEnumerable<int> x) => x.OmOfType<int>();
        sut.Should_not_enumerate_early();
    }

    [Fact]
    public void OmOfType_should_not_enumerate_all_when_not_demanded()
    {
        var sut = (IEnumerable<int> x) => x.OmOfType<int>().OmTake(2).OmToList();
        sut.Should_not_enumerate_all_when();
    }
}
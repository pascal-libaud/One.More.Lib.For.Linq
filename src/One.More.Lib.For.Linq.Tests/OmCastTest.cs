using One.More.Lib.For.Linq.Helper;

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
}

file abstract class DummyBase;

file class Dummy1 : DummyBase;

file class Dummy2 : DummyBase;
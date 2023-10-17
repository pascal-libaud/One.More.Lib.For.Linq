using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MySelectThrowEarlyTest
{
    [Fact]
    public void MySelectThrowEarly_should_throw_when_empty_before_enumeration()
    {
        IEnumerable<int> source = LinqHelper.Empty<int>();
        Assert.Throws<InvalidOperationException>(() => source.MySelectThrowEarly(x => x.ToString()));
    }

    [Fact]
    public void MySelectThrowEarly_should_work_well_when_not_empty()
    {
        var source = LinqHelper.Range(5).MySelectThrowEarly(x => x * 2);
        var expected = new[] { 0, 2, 4, 6, 8 };

        Assert.Equal(expected, source);
    }

    [Fact]
    public void MySelectThrowEarly_should_not_enumerate_twice()
    {
        var spy = new EnumerableSpy();

        var source = spy.GetValues().MySelectThrowEarly(x => x.ToString());

        // Assert.Equal(1, countEnumerations); // A ne pas tester ici car ce n'est pas ce qu'on cherche à vérifier

        _ = source.MyTake(5).MyToList();

        Assert.Equal(1, spy.CountEnumeration);
    }
}
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MyOrderByTest
{
    [Fact]
    public void MyOrderBy_should_sort_items()
    {
        var actual = new List<int> { 5, 4, 8, 1 }.MyOrderBy(x => x);
        var expected = new List<int> { 1, 4, 5, 8 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MyThenBy_should_not_reorder_items_sorted_by_MyOrderBy()
    {

        Person t1 = new("Tata", 5);
        Person t2 = new("Toto", 10);
        Person t3 = new("Tata", 12);
        Person t4 = new("Toto", 9);

        var actual = new List<Person> { t1, t2, t3, t4 }.MyOrderBy(x => x.Name).MyThenBy(x => x.Age);
        var expected = new List<Person> {t1, t3, t4, t2 };
        Assert.Equal(expected, actual);
    }
}

file record Person(string Name, int Age);
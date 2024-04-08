namespace One.More.Lib.For.Linq.Tests;

public class JoinTest : TestBase
{
    [Fact]
    public void Join_should_work_as_expected_1()
    {
        var list1 = new List<Outer> { new(1), new(2), new(3), new(4), new(5) };
        var list2 = new List<Inner> { new(2), new(4), new(6), new(8) };

        var expected = new List<Result> { new(2, 2), new(4, 4) };

        var result = list1.OmJoin(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).OmToList();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OmJoin_should_work_as_expected_2()
    {
        var list1 = new List<Outer> { new(1), new(2), new(1), new(2) };
        var list2 = new List<Inner> { new(2), new(1) };

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = list1.OmJoin(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).OmToList();
        result.Should().HaveCount(4);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OmJoin_should_work_as_expected_3()
    {
        var list1 = new List<Outer> { new(1), new(2) };
        var list2 = new List<Inner> { new(2), new(1), new(2), new(1) };

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = list1.OmJoin(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).OmToList();
        result.Should().HaveCount(4);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OmJoin_should_work_as_expected_4()
    {
        var list1 = new List<Outer> { new(1), new(2), new(1), new(2) };
        var list2 = new List<Inner> { new(2), new(1), new(2), new(1) };

        var expected = new List<Result> { new(1, 1), new(2, 2), new(1, 1), new(2, 2), new(1, 1), new(2, 2), new(1, 1), new(2, 2) };

        var result = list1.OmJoin(list2, x => x.Key, x => x.Key, (outer, inner) => new Result(outer.Key, inner.Key)).OmToList();
        result.Should().HaveCount(8);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void OmJoin_should_not_enumerate_early_on_outer_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => x.OmJoin(new List<int> { 1, 2 }, y => y, y => y, (y, z) => y + z);
        omJoin.Should_not_enumerate_early();
    }

    [Fact]
    public void OmJoin_should_not_enumerate_early_on_inner_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => new List<int> { 1, 2 }.OmJoin(x, y => y, y => y, (y, z) => y + z);
        omJoin.Should_not_enumerate_early();
    }

    [Fact]
    public void OmJoin_should_enumerate_each_ite_once_on_outer_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => x.OmJoin(new List<int> { 1, 2 }, y => y, y => y, (y, z) => y + z).OmToList();
        omJoin.Should_enumerate_each_item_once();
    }

    [Fact]
    public void OmJoin_should_enumerate_each_ite_once_on_inner_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => new List<int> { 1, 2 }.OmJoin(x, y => y, y => y, (y, z) => y + z).OmToList();
        omJoin.Should_enumerate_each_item_once();
    }

    [Fact]
    public void Join_should_not_enumerable_all_when_break_on_outer_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => x.OmJoin(new List<int> { 1, 2 }, y => y, y => y, (y, z) => y + z).OmTake(2).OmToList();
        omJoin.Should_not_enumerate_all_when();
    }

    // Trop lourd à mettre en place et Linq ne le gère pas non plus
    //[Fact]
    public void Join_should_not_enumerable_all_when_break_on_inner_enumerable()
    {
        var omJoin = (IEnumerable<int> x) => new List<int> { 1, 2 }.OmJoin(x, y => y, y => y, (y, z) => y + z).OmTake(1).OmToList();
        omJoin.Should_not_enumerate_all_when();
    }
}

file record Outer(int Key);
file record Inner(int Key);
file record Result(int OuterKey, int InnerKey);
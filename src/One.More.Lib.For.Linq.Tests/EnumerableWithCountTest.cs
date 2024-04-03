namespace One.More.Lib.For.Linq.Tests;

public class EnumerableWithCountTest : TestBase
{
    [Fact]
    public void EnumerableWithCount_should_not_evaluate_count_on_construction()
    {
        var spy = new SpyList<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        var append = spy.OmAppend(11);
        spy.Add(10);
        Assert.Equal(12, append.OmCount());
    }
}
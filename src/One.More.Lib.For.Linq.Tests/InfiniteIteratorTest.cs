using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class InfiniteIteratorTest
{
    [Fact]
    public async Task InfiniteIterator_should_not_make_infinite_loop()
    {
        var iterator = Task.Run(() => LinqHelper.InfiniteIterator<int>().MyTake(4).MyToList().ToTask());

        var delay = Task.Delay(1000);
        var result = await Task.WhenAny(iterator, delay);

        Assert.Equal(result, iterator);
    }
}
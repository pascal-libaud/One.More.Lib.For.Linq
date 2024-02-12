using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public static class TestHelper
{
    public static void Should_not_enumerate_early<T>(this Func<IEnumerable<int>, IEnumerable<T>> func)
    {
        var spy = new SpyEnumerable();

        var source = func(spy.GetValues());
        Assert.False(spy.IsEnumerated);

        _ = source.OmTake(5).OmToList();
        Assert.True(spy.IsEnumerated);
    }

    public static async Task Should_not_enumerate_early<T>(this Func<IAsyncEnumerable<int>, IAsyncEnumerable<T>> func)
    {
        var spy = new SpyEnumerable();

        var source = func(spy.GetValuesAsync());
        Assert.False(spy.IsEnumerated);

        _ = await source.OmTakeAsync(5).OmToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    // TODO : Rajouter le Should_enumerate_each_item_once
}
namespace One.More.Lib.For.Linq.Tests;

public static class TestHelper
{
    public static void Should_not_enumerate_early<T>(this Func<IEnumerable<int>, IEnumerable<T>> func)
    {
        var spy = SpyEnumerable.GetValues();

        var source = func(spy);
        Assert.False(spy.IsEnumerated);

        _ = source.OmTake(5).OmToList();
        Assert.True(spy.IsEnumerated);
    }

    public static async Task Should_not_enumerate_early<T>(this Func<IAsyncEnumerable<int>, IAsyncEnumerable<T>> func)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        var source = func(spy);
        Assert.False(spy.IsEnumerated);

        _ = await source.OmTakeAsync(5).OmToListAsync();
        Assert.True(spy.IsEnumerated);
    }

    public static void Should_enumerate_each_item_once<T>(this Func<IEnumerable<int>, T> func, ISpyEnumerable<int>? spy = null)
    {
        spy ??= SpyEnumerable.GetValues();

        _ = func(spy);
        Assert.Equal(1, spy.CountEnumeration);
    }

    public static async Task Should_enumerate_each_item_once<T>(this Func<IAsyncEnumerable<int>, Task<T>> func)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await func(spy);
        Assert.Equal(1, spy.CountEnumeration);
    }

    public static void Should_not_enumerate_all_when<T>(this Func<IEnumerable<int>, T> func)
    {
        var spy = SpyEnumerable.GetValues();

        _ = func(spy);
        Assert.False(spy.IsEndReached);
    }

    public static async Task Should_not_enumerate_all_when<T>(this Func<IAsyncEnumerable<int>, Task<T>> func)
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        _ = await func(spy);
        Assert.False(spy.IsEndReached);
    }
}
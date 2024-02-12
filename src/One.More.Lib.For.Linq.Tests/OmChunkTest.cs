using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmChunkTest : TestBase
{
    [Fact]
    public void OmChunk_should_work_as_expected()
    {
        var spy = new SpyEnumerable();

        var result = spy.GetValues().OmChunk(3).OmToList();

        Assert.Equal(4, result.Count);

        Assert.Equal(result[0], new List<int> { 0, 1, 2 });
        Assert.Equal(result[1], new List<int> { 3, 4, 5 });
        Assert.Equal(result[2], new List<int> { 6, 7, 8 });
        Assert.Equal(result[3], new List<int> { 9 });
    }

    [Fact]
    public void OmChunk_should_not_enumerate_early()
    {
        var spy = new SpyEnumerable();

        var source = spy.GetValues().OmChunk(3);
        Assert.False(spy.IsEnumerated);

        _ = source.OmTake(5).OmToList();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public void OmChunk_should_enumerate_each_item_once()
    {
        var spy = new SpyEnumerable();

        _ = spy.GetValues().OmChunk(3).OmToList();
        Assert.Equal(1, spy.CountEnumeration);
    }
}
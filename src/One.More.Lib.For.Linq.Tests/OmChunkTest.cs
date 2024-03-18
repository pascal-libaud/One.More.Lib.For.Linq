namespace One.More.Lib.For.Linq.Tests;

public class OmChunkTest : TestBase
{
    [Fact]
    public void OmChunk_should_work_as_expected()
    {
        var result = SpyEnumerable.GetValues().OmChunk(3).OmToList();

        Assert.Equal(4, result.Count);

        Assert.Equal(result[0], new List<int> { 0, 1, 2 });
        Assert.Equal(result[1], new List<int> { 3, 4, 5 });
        Assert.Equal(result[2], new List<int> { 6, 7, 8 });
        Assert.Equal(result[3], new List<int> { 9 });
    }

    [Fact]
    public void OmChunk_should_work_with_empty_source()
    {
        Assert.Equal(0, LinqHelper.Empty<int>().OmChunk(3).OmCount());
    }

    [Fact]
    public void OmChunk_should_not_return_last_empty_chunk()
    {
        Assert.Equal(3, LinqHelper.Range(9).OmChunk(3).OmCount());
    }

    [Fact]
    public void OmChunk_should_not_enumerate_early()
    {
        var omChunk = (IEnumerable<int> x) => x.OmChunk(3);
        omChunk.Should_not_enumerate_early();
    }

    [Fact]
    public void OmChunk_should_enumerate_each_item_once()
    {
        var omChunk = (IEnumerable<int> x) => x.OmChunk(3).OmToList();
        omChunk.Should_enumerate_each_item_once();
    }
}
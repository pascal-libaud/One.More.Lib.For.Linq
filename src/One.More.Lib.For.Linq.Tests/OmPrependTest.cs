using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmPrependTest : TestBase
{
    [Fact]
    public void OmPrepend_should_add_in_end_when_source_not_null()
    {
        static IEnumerable<string> GetValues()
        {
            yield return "P";
            yield return "Q";
        }

        Assert.Equal(new[] { "O", "P", "Q" }, GetValues().OmPrepend("O"));
    }

    [Fact]
    public void OmPrepend_should_work_well_when_source_empty()
    {
        static IEnumerable<string> GetValues()
        {
            yield break;
        }

        Assert.Equal(new[] { "R" }, GetValues().OmPrepend("R"));
    }

    [Fact]
    public void OmPrepend_should_not_throw_when_source_null_without_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.NotNull(() => GetValues().OmPrepend("R"));
    }

    [Fact]
    public void OmPrepend_should_throw_NullReferenceException_when_source_null_on_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.Throws<NullReferenceException>(() => GetValues().OmPrepend("R").OmToList());
    }
}
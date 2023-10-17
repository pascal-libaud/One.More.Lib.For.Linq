using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MyPrependTest
{
    [Fact]
    public void MyPrepend_should_add_in_end_when_source_not_null()
    {
        static IEnumerable<string> GetValues()
        {
            yield return "P";
            yield return "Q";
        }

        Assert.Equal(new[] { "O", "P", "Q" }, GetValues().MyPrepend("O"));
    }

    [Fact]
    public void MyPrepend_should_work_well_when_source_empty()
    {
        static IEnumerable<string> GetValues()
        {
            yield break;
        }

        Assert.Equal(new[] { "R" }, GetValues().MyPrepend("R"));
    }

    [Fact]
    public void MyPrepend_should_not_throw_when_source_null_without_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.NotNull(() => GetValues().MyPrepend("R"));
    }

    [Fact]
    public void MyPrepend_should_throw_NullReferenceException_when_source_null_on_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.Throws<NullReferenceException>(() => GetValues().MyPrepend("R").MyToList());
    }
}
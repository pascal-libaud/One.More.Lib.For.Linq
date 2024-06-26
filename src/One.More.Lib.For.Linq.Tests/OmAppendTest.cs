﻿namespace One.More.Lib.For.Linq.Tests;

public class OmAppendTest : TestBase
{
    [Fact]
    public void OmAppend_should_add_in_end_when_source_not_null()
    {
        static IEnumerable<string> GetValues()
        {
            yield return "P";
            yield return "Q";
        }

        Assert.Equal(new[] { "P", "Q", "R" }, GetValues().OmAppend("R"));
        Assert.Equal(new[] { "P", "Q", "R" }, GetValues().OmToList().OmAppend("R"));
    }

    [Fact]
    public void OmAppend_should_work_well_when_source_empty()
    {
        static IEnumerable<string> GetValues()
        {
            yield break;
        }

        Assert.Equal(new[] { "R" }, GetValues().OmAppend("R"));
        Assert.Equal(new[] { "R" }, GetValues().OmToList().OmAppend("R"));
    }

    [Fact]
    public void OmAppend_should_not_throw_when_source_null_without_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.NotNull(() => GetValues().OmAppend("R"));
        Assert.NotNull(() => GetValues().OmToList().OmAppend("R"));
    }

    [Fact]
    public void OmAppend_should_throw_NullReferenceException_when_source_null_on_enumeration()
    {
        static IEnumerable<string> GetValues()
        {
            return null!;
        }

        Assert.Throws<NullReferenceException>(() => GetValues().OmAppend("R").OmToList());
    }
}
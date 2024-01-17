using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class AlphabetTest
{
    [Theory]
    [InlineData(EnumerationWay.For)]
    [InlineData(EnumerationWay.Range)]
    internal void Alphabet_should_return_only_capital_letters(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        var alphabet = LinqHelper.Alphabet();

        Action<char> isCapitalLetter = c => Assert.True(c >= 'A' && c <= 'Z');

        Assert.All(alphabet, isCapitalLetter);
    }

    [Theory]
    [InlineData(EnumerationWay.For)]
    [InlineData(EnumerationWay.Range)]
    internal void Alphabet_should_return_all_capital_letters(EnumerationWay way)
    {
        EnumerationWayStrategy.FocusOn = way;

        var alphabet = LinqHelper.Alphabet().OmToList();
        for (char c = 'A'; c <= 'Z'; c++)
            Assert.Contains(alphabet, x => x == c);
    }
}
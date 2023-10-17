using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class AlphabetTest
{
    [Fact]
    public void Alphabet_should_return_only_capital_letters()
    {
        var alphabet = LinqHelper.Alphabet();

        Action<char> isCapitalLetter = c => Assert.True(c >= 'A' && c <= 'Z');

        Assert.All(alphabet, isCapitalLetter);
    }

    [Fact]
    public void Alphabet_should_return_all_capital_letters()
    {
        var alphabet = LinqHelper.Alphabet().MyToList();
        for (char c = 'A'; c <= 'Z'; c++)
            Assert.Contains(alphabet, x => x == c);
    }
}
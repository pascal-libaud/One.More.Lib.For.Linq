namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<char> Alphabet()
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.For => Alphabet_For(),
            EnumerationWay.Range => Alphabet_Range(),
            _ => Alphabet_For()
        };
    }

    private static IEnumerable<char> Alphabet_For()
    {
        for (char c = 'A'; c <= 'Z'; c++)
            yield return c;
    }

    private static IEnumerable<char> Alphabet_Range()
    {
        return Range('A', (char)('Z' - 'A' + 1));
    }
}
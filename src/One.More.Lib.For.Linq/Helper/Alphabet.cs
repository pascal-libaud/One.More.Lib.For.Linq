namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<char> Alphabet()
    {
        for (char c = 'A'; c <= 'Z'; c++)
            yield return c;
    }
}
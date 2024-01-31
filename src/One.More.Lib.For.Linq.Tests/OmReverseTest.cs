using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmReverseTest : TestBase
{
    [Fact]
    public void OmReverse_should_reverse_alphabet()
    {
        var expected = new[] { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N',
            'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A' };
        Assert.Equal(expected, LinqHelper.Alphabet().OmReverse());
    }
}
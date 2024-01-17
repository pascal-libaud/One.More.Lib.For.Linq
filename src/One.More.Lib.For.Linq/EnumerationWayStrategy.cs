
namespace One.More.Lib.For.Linq;

internal static class EnumerationWayStrategy
{
    public static EnumerationWay FocusOn = EnumerationWay.Foreach;
}

internal enum EnumerationWay
{
    For,
    Foreach,
    Enumerator,
    Range,
}
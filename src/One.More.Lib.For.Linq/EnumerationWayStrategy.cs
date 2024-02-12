namespace One.More.Lib.For.Linq;

internal static class EnumerationWayStrategy
{
    internal static EnumerationWay FocusOn = EnumerationWay.Foreach;
}

internal enum EnumerationWay
{
    For,
    Foreach,
    Enumerator,
    Range,
}
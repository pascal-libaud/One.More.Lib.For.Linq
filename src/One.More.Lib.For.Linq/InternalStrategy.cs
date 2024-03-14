namespace One.More.Lib.For.Linq;

internal static class InternalStrategy
{
    internal static EnumerationWay Selected = EnumerationWay.Foreach;

    internal static readonly EnumerationWay Default = EnumerationWay.Foreach;
}

internal enum EnumerationWay
{
    For,
    Foreach,
    Enumerator,
    Range,
}
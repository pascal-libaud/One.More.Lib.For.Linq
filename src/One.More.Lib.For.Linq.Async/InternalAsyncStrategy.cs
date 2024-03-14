namespace One.More.Lib.For.Linq.Async;

internal static class InternalAsyncStrategy
{
    internal static AsyncEnumerationWay Selected = AsyncEnumerationWay.Foreach;

    internal static readonly AsyncEnumerationWay Default = AsyncEnumerationWay.Foreach;
}

internal enum AsyncEnumerationWay
{
    For,
    Foreach,
    Enumerator,
    Range,
}
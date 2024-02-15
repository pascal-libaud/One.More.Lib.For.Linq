namespace One.More.Lib.For.Linq.Tests;

public interface ISpy
{
    bool IsEnumerated { get; }
    bool IsEndReached { get; set; }
    int CountEnumeration { get; set; }
    int CountItemEnumerated { get; set; }
}

public interface ISpyEnumerable<T> : ISpy, IEnumerable<T> { }
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class FullOuterJoinTest
{
    [Theory]
    [MemberData(nameof(GetValues))]
    public void Test(Mock1[] mocks1, Mock2[] mocks2, Result[] expected)
    {
        Func<Mock1?, Mock2?, int> comparer = (a, b) =>
        {
            return (a, b) switch
            {
                (null, null) => 0,
                (_, null) => -1,
                (null, _) => 1,
                (_, _) => a.Id.CompareTo(b.Id)
            };
        };

        var result = mocks1.FullOuterJoin(mocks2, comparer).OmSelect(x => new Result(x.Item1?.Id, x.Item2?.Id));

        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetValues()
    {
        var m11 = new Mock1(1, "A", 1);
        var m12 = new Mock1(2, "B", 1);
        var m14 = new Mock1(4, "C", 1);

        var m21 = new Mock2(1, "Z");
        var m23 = new Mock2(3, "Y");
        var m24 = new Mock2(4, "X");

        var mocks1 = new[] { m11, m12, m14 };
        var mocks2 = new[] { m21, m23, m24 };

        var expected = new Result[]
        {
            new(1, 1),
            new(2, null),
            new(null, 3),
            new(4, 4)
        };

        yield return new object[] { mocks1, mocks2, expected };

        mocks1 = Array.Empty<Mock1>();

        expected = new Result[]
        {
            new(null, 1),
            new(null, 3),
            new(null, 4)
        };

        yield return new object[] { mocks1, mocks2, expected };

        mocks1 = new[] { m11, m12, m14 };
        mocks2 = Array.Empty<Mock2>();

        expected = new Result[]
        {
            new(1, null),
            new(2, null),
            new(4, null)
        };

        yield return new object[] { mocks1, mocks2, expected };
    }
}

public record Mock1(int Id, string Name, int Age);
public record Mock2(int Id, string Name);
public record Result(int? Id1, int? Id2);
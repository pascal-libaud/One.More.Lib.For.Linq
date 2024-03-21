namespace One.More.Lib.For.Linq.Tests;

// TODO Quand xunit v3 sort, passer à [assembly: AssemblyFixture]

[CollectionDefinition(nameof(MyCollection))]
public class MyCollection : ICollectionFixture<StrategyFixture>;

public class StrategyFixture
{
    public StrategyFixture()
    {
        string? value = Environment.GetEnvironmentVariable("EnumerationStrategy");

        if (value != null && Enum.TryParse<EnumerationWay>(value, true, out var way))
            InternalStrategy.Selected = way;
        else
            InternalStrategy.Selected = InternalStrategy.Default;
    }
}

[Collection(nameof(MyCollection))]
public abstract class TestBase;
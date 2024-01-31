namespace One.More.Lib.For.Linq.Tests;

// TODO Quand xunit v3 sort, passer � [assembly: AssemblyFixture]

[CollectionDefinition(nameof(MyCollection))]
public class MyCollection : ICollectionFixture<StrategyFixture>;

public class StrategyFixture
{
    public StrategyFixture()
    {
        string? value = Environment.GetEnvironmentVariable("EnumerationStrategy");

        if (value != null && Enum.TryParse<EnumerationWay>(value, true, out var way))
            EnumerationWayStrategy.FocusOn = way;
        else
            EnumerationWayStrategy.FocusOn = EnumerationWay.Foreach;
    }
}

[Collection(nameof(MyCollection))]
public abstract class TestBase;
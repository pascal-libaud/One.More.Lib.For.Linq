using System;
using System.Collections.Generic;
using One.More.Lib.For.Linq.Helper;

namespace HelloWorld;

internal class Program
{
    static void Main()
    {
        foreach (var c in LinqHelper.Alphabet())
            Console.Write(c);

        Console.WriteLine();

        foreach (var prime in PrimeNumbers().OmTake(100))
        {
            Console.WriteLine(prime);
        }
    }

    private static IEnumerable<int> PrimeNumbers()
    {
        var primes = new List<int>();

        foreach (var number in LinqHelper.InfiniteIterator(2))
        {
            if (primes.OmAll(i => number % i != 0))
            {
                primes.Add(number);
                yield return number;
            }
        }
    }
}
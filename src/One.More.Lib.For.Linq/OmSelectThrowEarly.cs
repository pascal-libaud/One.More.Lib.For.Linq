﻿namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<U> OmSelectThrowEarly<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        // Version qui énumère 2x la collection
        //if(!source.Any())
        //    throw new InvalidOperationException("The sequence contains no element");

        //return Internal(source, selector);

        //static IEnumerable<U> Internal(IEnumerable<T> s, Func<T, U> selector)
        //{
        //    foreach (var item in s)
        //    {
        //        yield return selector(item);
        //    }
        //}

        /*using*/
        var enumerator = source.GetEnumerator(); // On ne peut pas faire le using sinon l'Enumerator est Dispose à la sortie et l'énumeration est vide

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("The sequence contains no element");

        return Enumerate(enumerator, selector);

        static IEnumerable<U> Enumerate(IEnumerator<T> enumerator, Func<T, U> selector)
        {
            do
            {
                yield return selector(enumerator.Current);
            } while (enumerator.MoveNext());
        }
    }
}
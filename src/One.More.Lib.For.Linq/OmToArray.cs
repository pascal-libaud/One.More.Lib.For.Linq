namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T[] OmToArray<T>(this IEnumerable<T> source)
    {
        if (source is ICollection<T> collection)
        {
            T[] array = new T[collection.Count];
            collection.CopyTo(array, 0);
            return array;
        }

        if (source is IWithCount withCount)
        {
            T[] array = new T[withCount.Count];

            foreach(var (index, item) in source.OmIndex())
                array[index] = item;

            return array;
        }

        return source.OmToList().ToArray();
    }
}
using System;

namespace Lab1.App.Services
{
    public static class Arrays
    {
        public static T[] Add<T>(T[] source, T item)
        {
            T[] result = new T[source.Length + 1];
            Array.Copy(source, result, source.Length);
            result[source.Length] = item;
            return result;
        }

        public static T[] AddRange<T>(T[] source, T[] items)
        {
            T[] result = new T[source.Length + items.Length];
            Array.Copy(source, result, source.Length);
            Array.Copy(items, 0, result, source.Length, items.Length);
            return result;
        }
    }
}

using System;
using System.Collections;
using System.Linq;

namespace PizzaMaker.Code.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable enumerable, Action<T> action)
        {
            enumerable.OfType<T>().ToList().ForEach(action);
        }
    }
}
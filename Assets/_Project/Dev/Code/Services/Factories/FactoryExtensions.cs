using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PizzaMaker.Code.Services.Factories
{
    public static class FactoryExtensions
    {
        public static List<T> DoMultiple<T>(int count, Func<T> create)
        {
            var list = new List<T>();

            for (int i = 0; i < count; i++)
            {
                list.Add(create());
            }

            return list;
        }
        
        public static IEnumerable<T> SetParent<T>(this IEnumerable<T> children, Transform parent) where T : Component
        {
            var list = children.ToList();

            list.ForEach(x => x.transform.SetParent(parent));

            return list;
        }
        
        public static IEnumerable<GameObject> SetParent(this IEnumerable<GameObject> children, Transform parent)
        {
            var list = children.ToList();

            list.ForEach(x => x.transform.SetParent(parent));

            return list;
        }
    }
}
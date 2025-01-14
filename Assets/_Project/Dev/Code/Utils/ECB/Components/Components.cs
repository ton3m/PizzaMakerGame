using System;
using System.Collections.Generic;

namespace PizzaMaker.Code.Utils.ECB
{
    public class Components : IComponentsProvider
    {
        private readonly Dictionary<Type, object> _components = new();

        public T Get<T>()
        {
            if (!_components.ContainsKey(typeof(T)))
                throw new Exception($"Component {typeof(T)} not found");

            return (T)_components[typeof(T)];
        }

        public bool TryGet<T>(out T component)
        {
            if (_components.ContainsKey(typeof(T)))
            {
                component = (T)_components[typeof(T)];
                return true;
            }
            
            component = default;
            return false;
        }
        
        public bool Has<T>()
        {
            return _components.ContainsKey(typeof(T));
        }

        public Components Add<T>(T component)
        {
            if (_components.ContainsKey(typeof(T)))
                throw new Exception($"Component {typeof(T)} already added");

            _components.Add(typeof(T), component);
            
            return this;
        }

        public void Set<T>(T component)
        {
            if (!_components.ContainsKey(typeof(T)))
                throw new Exception($"Component {typeof(T)} not found");

            _components[typeof(T)] = component;
        }
    }
}
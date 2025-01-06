using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Services;

namespace PizzaMaker.Code.Core.Character
{
    public class CharacterEntity : IComponentsProvider, IUpdatable, IDisposable
    {
        private readonly List<object> _behaviours = new();
        private readonly Dictionary<Type, object> _components = new();

        private readonly List<IUpdatable> _updatables = new();
        private readonly List<IDisposable> _disposables = new();

        public CharacterEntity Initialize()
        {
            _behaviours
                .Select(x => x as IInitBehaviour)
                .ToList()
                .ForEach(x => x.Initialize(this));
            
            _updatables.AddRange(_behaviours.OfType<IUpdatable>());
            _disposables.AddRange(_behaviours.OfType<IDisposable>());
            
            return this;
        }

        public CharacterEntity AddBehaviour(object behaviour)
        {
            _behaviours.Add(behaviour);
            return this;
        }

        public CharacterEntity AddComponent<T>(T component)
        {
            _components.Add(typeof(T), component);
            return this;
        }

        public T GetComponent<T>()
        {
            if (!_components.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Component {typeof(T)} not found");
            
            return (T)_components[typeof(T)];
        }

        public void Update() =>
            _updatables.ForEach(x => x.Update());

        public void Dispose() => 
            _disposables.ForEach(x => x.Dispose());
    }

    public interface IInitBehaviour
    {
        void Initialize(IComponentsProvider entity);
    }
}
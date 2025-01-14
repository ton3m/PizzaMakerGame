using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Utils.DI
{
    public class DIContainer : IDisposable
    {
        private Event _initialized = new();
        
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly DIContainer _parent;

        private readonly List<Type> _requests = new();

        public DIContainer() : this(null)
        {
        }

        public DIContainer(DIContainer parent) => _parent = parent;

        public IObservable Initialized => _initialized;
        
        public Registration RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} already register");

            Registration registration = new Registration(container => creator(container));
            _container[typeof(T)] = registration;
            return registration;
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return CreateFrom<T>(registration);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exist");
        }

        public void Initialize()
        {
            foreach (Registration registration in _container.Values.ToList())
            {
                if (registration.Instance == null && registration.IsNonLazy)
                    registration.Instance = registration.Creator(this);

                if (registration.Instance != null)
                    if (registration.Instance is IInitializable initializable)
                        initializable.Initialize();
            }
            
            _initialized.Notify();
        }

        public void Dispose()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.Instance != null)
                    if (registration.Instance is IDisposable disposable)
                        disposable.Dispose();
            }
        }

        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Creator != null)
                registration.Instance = registration.Creator(this);

            return (T)registration.Instance;
        }

        public class Registration
        {
            public Func<DIContainer, object> Creator { get; }
            public object Instance { get; set; }
            public bool IsNonLazy { get; private set; }

            public Registration(object instance) => Instance = instance;

            public Registration(Func<DIContainer, object> creator) => Creator = creator;

            public void NonLazy() => IsNonLazy = true;
        }
    }

    public interface IInitializable
    {
        public void Initialize();
    }
}
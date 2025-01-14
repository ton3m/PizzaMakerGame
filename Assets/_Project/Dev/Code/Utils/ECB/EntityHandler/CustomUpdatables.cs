using System;
using System.Collections.Generic;
using PizzaMaker.Code.Utils.Common;

namespace PizzaMaker.Code.Utils.ECB
{
    public class CustomUpdatables
    {
        private Dictionary<Type, Func<IUpdatable, IUpdatable>> _dictionary = new();

        public IReadOnlyDictionary<Type, Func<IUpdatable, IUpdatable>> Dictionary => _dictionary;

        public CustomUpdatables Add<T>(Action<IUpdatable> onUpdate) where T : IUpdatable
        {
            Func<IUpdatable, IUpdatable> func =
                updatable => new UpdatableAction(() => onUpdate(updatable));

            _dictionary.Add(typeof(T), func);

            return this;
        }

        public CustomUpdatables Add<T>(Func<bool> condition) where T : IUpdatable
        {
            Action<IUpdatable> action = updatable =>
            {
                if (condition())
                    updatable.Update();
            };
            
            return Add<T>(action);
        }
    }
}
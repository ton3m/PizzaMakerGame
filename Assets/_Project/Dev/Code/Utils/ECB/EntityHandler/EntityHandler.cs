using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.Common;
using PizzaMaker.Code.Utils.Extensions;

namespace PizzaMaker.Code.Utils.ECB
{
    public class EntityHandler : IEntityHandler
    {
        private List<IUpdatable> _updatables;
        private List<IDisposable> _disposables;

        private readonly IEntity _entity;
        private readonly List<object> _behaviours;

        public EntityHandler(IEntity entity)
        {
            _entity = entity;
            _behaviours = _entity.Behaviours.List;

            HandleInitialize();
            HandleUpdate();
            HandleDispose();
            
            HandleCustomUpdate();
        }

        private void HandleDispose() =>
            _disposables = _behaviours.OfType<IDisposable>().ToList();

        private void HandleUpdate() => 
            _updatables = _behaviours.OfType<IUpdatable>().ToList();

        private void HandleCustomUpdate()
        {
            if (_entity.Components.TryGet<CustomUpdatables>(out var customs))
            {
                for (int i = 0; i < _updatables.Count; i++)
                {
                    var type = _updatables[i].GetType();
                    
                    if (customs.Dictionary.TryGetValue(type, out var customUpdate))
                    {
                        _updatables[i] = customUpdate(_updatables[i]);
                    }
                }
            }
        }

        private void HandleInitialize() =>
            _behaviours.ForEach<IComponentsInit>(x => x.Initialize(_entity.Components));

        public void Update() => _updatables?.ForEach(x => x.Update());

        public void Dispose() => _disposables?.ForEach(x => x.Dispose());
    }
}
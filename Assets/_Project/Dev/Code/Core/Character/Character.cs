using System;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Services.Factories;
using PizzaMaker.Code.Services.Factories.Character;
using PizzaMaker.Code.Utils.ECB;
using PizzaMaker.Code.Utils.Reactive.Deprecated;
using Common_IUpdatable = PizzaMaker.Code.Utils.Common.IUpdatable;

namespace PizzaMaker.Code.Core.Character
{
    public class Character : Common_IUpdatable, IDisposable
    {
        private readonly IEntity _entity;
        private readonly IEntityHandler _handler;

        public Character(IEntity entity, IEntityHandler handler)
        {
            _handler = handler;
            _entity = entity;
        }
        
        public bool Enabled
        {
            get => _entity.Components.Get<EntityEnabled>().Enabled;
            set => _entity.Components.Set<EntityEnabled>(new() { Enabled = value });
        }

        public ICollisionDetector FinishCollisionDetector => 
            _entity.Components.Get<FinishCollisionDetector>().CollisionDetector;

        public IReadOnlyReactiveVar<float> LevelProgress => new ReactiveVar<float>(0);
        
        public void Update() => _handler.Update();

        public void Dispose() => _handler.Dispose();
    }
}
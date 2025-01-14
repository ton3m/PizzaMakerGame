using System.Collections;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services.Factories;
using PizzaMaker.Code.Services.Factories.Character;
using PizzaMaker.Code.Utils.DI;

namespace PizzaMaker.Code.Entry.Bootstraps.Test
{
    public class SnakeTestBootstrap : SceneBootstrap
    {
        private Character _character;
        
        public override IEnumerator Run(DIContainer container)
        {
            var entitiesFactory = new EntityFactory(container);
            
            _character = new CharacterFactory(entitiesFactory).CreatePizzaCharacter();
            
            yield return null;
        }

        private void Update() => _character?.Update();
        
        private void OnDestroy() => _character?.Dispose();
    }
}
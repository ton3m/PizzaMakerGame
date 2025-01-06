using System.Collections;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services.CharacterFactory;
using PizzaMaker.Code.Utils.DI;

namespace PizzaMaker.Code.Entry.Bootstraps.SceneBootstraps
{
    public class GameplayTestBootstrap : SceneBootstrap
    {
        private CharacterEntity _character;
        
        public override IEnumerator Run(DIContainer container)
        {
            var entitiesFactory = new EntitiesFactory(container);
            
            _character = entitiesFactory.CreateCharacter();
            
            yield return null;
        }

        private void Update() => _character?.Update();
        
        private void OnDestroy() => _character?.Dispose();
    }
}
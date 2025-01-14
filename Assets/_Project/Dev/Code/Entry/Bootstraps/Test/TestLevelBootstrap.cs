using System.Collections;
using System.Collections.Generic;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Tags;
using PizzaMaker.Code.Services.EventsHandling;
using PizzaMaker.Code.Services.Factories;
using PizzaMaker.Code.Services.Factories.Character;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.StateSwitching.Gameplay;
using PizzaMaker.Code.Services.StateSwitching.Level;
using PizzaMaker.Code.Utils.Common;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Entry.Bootstraps.Test
{
    public class TestLevelBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private readonly List<IUpdatable> _updatables = new();

        public override IEnumerator Run(DIContainer container)
        {
            _container = container;

            RegUIStateMachine();
            RegEventsHandleFactory();
            RegDisposer();

            RegCharacter();
            RegGameEndObserver();

            RegLevelData();
            RegGameStateHolder();

            _container.Initialize();

            yield return null;
        }

        private void Update() =>
            _updatables.ForEach(x => x.Update());

        private void OnDestroy() =>
            _container?.Resolve<IDisposer>().Dispose();

        private void RegLevelData() =>
            _container.RegisterAsSingle(c =>
                new LevelData(c.Resolve<Character>().LevelProgress, 1));

        private void RegGameEndObserver() =>
            _container.RegisterAsSingle(c =>
                new GameEndObserver(
                    new FinishLinePassedObserver(c.Resolve<Character>().FinishCollisionDetector).IsPassed,
                    new Reactive<bool>()));

        private void RegEventsHandleFactory() =>
            _container.RegisterAsSingle(c =>
            {
                var factory = new EventsHandleFactory();

                factory.HandleAll(c).DisposeIn(c.Resolve<IDisposer>());

                return factory;
            }).NonLazy();

        private void RegCharacter()
        {
            _container.RegisterAsSingle(c =>
            {
                var position = FindAnyObjectByType<CharacterSpawnPoint>().transform.position;

                var character = new CharacterFactory(new EntityFactory(c))
                    .CreateDoughCharacter(position);

                character.Enabled = false;

                _updatables.Add(character);
                _container.Resolve<IDisposer>().Add(character);

                return character;
            }).NonLazy();
        }

        private void RegDisposer() =>
            _container.RegisterAsSingle<IDisposer>(c => new Disposer());

        private void RegGameStateHolder() =>
            _container.RegisterAsSingle(c => new GameplayStateHolder());

        private void RegUIStateMachine() =>
            _container.RegisterAsSingle(c => new UIFactory(c).CreateUIStateMachine());
    }
}
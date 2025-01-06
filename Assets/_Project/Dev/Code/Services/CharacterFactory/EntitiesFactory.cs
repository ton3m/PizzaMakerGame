using System.Linq;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.SideMovement;
using PizzaMaker.Code.Core.SnakeMovement;
using PizzaMaker.Code.Services.Input;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Utils.DI;
using UnityEngine;

namespace PizzaMaker.Code.Services.CharacterFactory
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
        }

        public CharacterEntity CreateCharacter()
        {
            CharacterFactory factory = new(_container.Resolve<ResourcesLoader>());

            var character = factory.CreateCharacterTemplate();

            var doughs = factory
                .DoMultiple(factory.CreateDough, 10)
                .SetParent(character.transform);

            var segments = doughs.Select(x => x.transform).ToList();

            var colliders = segments
                .Select(x => x.gameObject.AddComponent<SphereCollider>())
                .ToList();
            
            var rigidBodies = segments
                .Select(x => x.gameObject.AddComponent<Rigidbody>())
                .ToList();
            
            var collisions = segments
                .Select(x => x.gameObject.AddComponent<CollisionDetector>())
                .Cast<ICollisionDetector>().ToList();
            
            colliders.ForEach(x =>
            {
                x.radius = 1f;
                x.isTrigger = true;
            });
            
            rigidBodies.ForEach(x =>
            {
                x.useGravity = false;
                x.isKinematic = true;
            });

            var transformsData = new SnakeTransformsData(segments, character.Body);
            var collisionData = new SnakeCollisionData(collisions);
            var structureData = new CharacterStructureData(character.Origin, character.Body);

            var entity = new CharacterEntity()
                .AddBehaviour(new SideMoveBehaviour())
                .AddBehaviour(new SnakeMoveBehaviour())
                .AddBehaviour(new SnakeCollisionHandler())
                .AddComponent(SideMoveConfig.Default)
                .AddComponent(SnakeMoveConfig.Default)
                .AddComponent<ISideDragInput>(new SideDragInput())
                .AddComponent(structureData)
                .AddComponent(transformsData)
                .AddComponent(collisionData)
                .Initialize();

            return entity;
        }
    }
}
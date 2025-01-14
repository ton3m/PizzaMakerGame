using System;
using System.Linq;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.DoughScaler;
using PizzaMaker.Code.Core.ForwardMovement;
using PizzaMaker.Code.Core.SideMovement;
using PizzaMaker.Code.Core.SnakeCollision;
using PizzaMaker.Code.Core.SnakeMovement;
using PizzaMaker.Code.Services.Input;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.ECB;
using UnityEngine;

namespace PizzaMaker.Code.Services.Factories.Character
{
    public class EntityFactory
    {
        private readonly DIContainer _container;

        public EntityFactory(DIContainer container)
        {
            _container = new(container);

            _container.RegisterAsSingle(c =>
                new ObjectsFactory(c.Resolve<ResourcesLoader>()));
        }

        public Entity CreatePizzaSnakeEntity(Vector3 position = default)
        {
            var entity = CreateBaseCharacterEntity(position);
            var structure = entity.Components.Get<CharacterStructureData>();
            
            var prefab = _container.Resolve<ObjectsFactory>().PizzaPrefab;
            var snakeFactory = new SnakeFactory();
            
            var segments = snakeFactory.CreateSnakeSegments(position, prefab, structure.Origin);

            var collisions = segments
                .Select(x => x.gameObject.GetComponent<CollisionDetector>())
                .Cast<ICollisionDetector>().ToList();

            entity.Behaviours
                .Add(new SnakeMoveBehaviour())
                .Add(new SnakeCollisionHandler());

            entity.Components
                .Add(SnakeMoveData.Default)
                .Add(new SnakeSegmentsData(segments, structure.Body))
                .Add(new SnakeCollisionData(collisions));
            
            return entity;
        }

        public IEntity CreateDoughEntity(Vector3 position)
        {
            var entity = CreateBaseCharacterEntity(position);
            
            var dough = _container.Resolve<ObjectsFactory>().CreateDough(position);
            
            var body = entity.Components.Get<CharacterStructureData>().Body;

            dough.transform.SetParent(body);

            var collision = dough.CollisionDetector;
            var scalable = dough.Scalable;

            entity.Behaviours
                .Add(new DoughCollisionHandlerScaler());

            entity.Components
                .Add(new Scalable { Transform = scalable })
                .Add(new FinishCollisionDetector { CollisionDetector = collision });

            return entity;
        }

        private Entity CreateBaseCharacterEntity(Vector3 position = default)
        {
            var factory = _container.Resolve<ObjectsFactory>();

            var character = factory.CreateCharacterTemplate();

            var entity = new Entity();
            
            entity.Behaviours
                .Add(new SideMoveBehaviour())
                .Add(new ForwardMoveBehaviour());

            entity.Components
                .Add<ISideDragInput>(new SideDragInput())
                .Add(new CharacterStructureData(character.Origin, character.Body))
                .Add(new EntityEnabled())
                .Add(new SideMoveData(5f, 2.5f))
                .Add(new ForwardMoveSpeed { Value = 3f });


            Func<bool> condition = () => entity.Components.Get<EntityEnabled>().Enabled;

            var customUpdatables = new CustomUpdatables()
                .Add<ForwardMoveBehaviour>(condition)
                .Add<SideMoveBehaviour>(condition);

            entity.Components.Add(customUpdatables);
            
            return entity;
        }
    }
}
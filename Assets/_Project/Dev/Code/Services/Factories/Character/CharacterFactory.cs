using PizzaMaker.Code.Utils.ECB;
using UnityEngine;

namespace PizzaMaker.Code.Services.Factories.Character
{
    public class CharacterFactory
    {
        private readonly EntityFactory _entityFactory;

        public CharacterFactory(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public Core.Character.Character CreatePizzaCharacter(Vector3 position = default)
        {
            var entity = _entityFactory.CreatePizzaSnakeEntity(position);
            var handler = new EntityHandler(entity);
            
            return new Core.Character.Character(entity, handler);
        }

        public Core.Character.Character CreateDoughCharacter(Vector3 position = default)
        {
            var entity = _entityFactory.CreateDoughEntity(position);
            
            return new Core.Character.Character(entity, new EntityHandler(entity));
        }
    }
}
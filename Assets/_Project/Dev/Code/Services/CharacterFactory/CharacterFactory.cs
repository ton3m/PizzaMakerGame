using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services.Loaders;
using UnityEngine;

namespace PizzaMaker.Code.Services.CharacterFactory
{
    public class CharacterFactory
    {
        private readonly ResourcesLoader _resourcesLoader;

        public CharacterFactory(ResourcesLoader resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;
        }

        public Character CreateCharacterTemplate()
        {
            var prefab = _resourcesLoader.LoadResource<Character>(ResourcesPaths.CharacterTemplatePrefab);

            var instance = UnityEngine.Object.Instantiate(prefab);

            return instance;
        }

        public GameObject CreateDough()
        {
            var prefab = _resourcesLoader.LoadResource<GameObject>(ResourcesPaths.DoughPrefab);

            var instance = UnityEngine.Object.Instantiate(prefab);

            return instance;
        }
    }
}
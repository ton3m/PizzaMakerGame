using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.DoughScaler;
using PizzaMaker.Code.Services.Loaders;
using UnityEngine;

namespace PizzaMaker.Code.Services.Factories
{
    public class ObjectsFactory
    {
        private readonly ResourcesLoader _resourcesLoader;

        public ObjectsFactory(ResourcesLoader resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;
        }

        public GameObject PizzaPrefab => _resourcesLoader.Load<GameObject>(ResourcesPaths.PizzaPrefab);
        
        private DoughStructure DoughPrefab => _resourcesLoader.Load<DoughStructure>(ResourcesPaths.DoughPrefab);
        private CharacterStructure CharacterTemplatePrefab => _resourcesLoader.Load<CharacterStructure>(ResourcesPaths.CharacterTemplatePrefab);
        
        public CharacterStructure CreateCharacterTemplate(Vector3 position = default)
        {
            var instance = Object.Instantiate(CharacterTemplatePrefab);

            instance.transform.position = position;

            return instance;
        }

        public DoughStructure CreateDough(Vector3 position = default)
        {
            var instance = Object.Instantiate(DoughPrefab);

            instance.transform.position = position;

            return instance;
        }

        public GameObject CreatePizza(Vector3 position = default)
        {
            var instance = Object.Instantiate(PizzaPrefab);

            instance.transform.position = position;

            return instance;
        }
    }
}
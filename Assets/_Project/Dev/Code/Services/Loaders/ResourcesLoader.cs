using UnityEngine;

namespace PizzaMaker.Code.Services.Loaders
{
    public class ResourcesLoader 
    {
        public T Load<T>(string path) where T : Object
        {
            T resource = Resources.Load<T>(path);
            
            if (resource == null) 
                Debug.LogError($"Resource {path} not found");
            
            return resource;
        }
    }
}

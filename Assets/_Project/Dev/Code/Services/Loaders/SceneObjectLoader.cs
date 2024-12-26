using System;

namespace PizzaMaker.Code.Services.Loaders
{
    public class SceneObjectLoader
    {
        public T Load<T>() where T : UnityEngine.Object
        {
            T obj = UnityEngine.Object.FindAnyObjectByType<T>();

            if (obj == null)
                throw new NullReferenceException($"GameObject of type {typeof(T).Name} not found.");

            return obj;
        }
    }
}
using System.Collections;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    public static class SceneLoaderExtensions
    {
        public static IEnumerator LoadAsync(this ISceneLoader loader, SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return loader.LoadAsync(sceneId.ToString(), mode);
        }

        public static void Load(this ISceneLoader loader, SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single)
        {
            loader.Load(sceneId.ToString(), mode);
        }

        public static IEnumerator UnloadAsync(this ISceneLoader loader, SceneId sceneId, UnloadSceneOptions options = UnloadSceneOptions.None)
        {
            return loader.UnloadAsync(sceneId.ToString(), options);
        }   
    }
}
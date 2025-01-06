using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    namespace PizzaMaker.Code.Services.Loaders.Scene
    {
        public class SceneLoader : ISceneLoader
        {
            public IEnumerator LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
            {
                var operation = SceneManager.LoadSceneAsync(sceneName, mode);

                if (operation == null)
                    throw new ArgumentException($"Scene {sceneName} not found in build scene list");

                yield return operation;

                Debug.Log($"Scene {sceneName} loaded.");
            }

            public IEnumerator UnloadAsync(string sceneName, UnloadSceneOptions options = UnloadSceneOptions.None)
            {
                var operation = SceneManager.UnloadSceneAsync(sceneName, options);

                if (operation == null)
                    throw new ArgumentException($"Scene {sceneName} not found in build scene list");

                yield return operation;

                Debug.Log($"Scene {sceneName} unloaded.");
            }

            public void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
            {
                SceneManager.LoadScene(sceneName, mode);
                Debug.Log($"Scene {sceneName} loaded.");
            }
        }
    }
}


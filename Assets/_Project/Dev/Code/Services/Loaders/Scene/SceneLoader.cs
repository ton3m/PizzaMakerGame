using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    public class SceneLoader : ISceneLoader
    {
        public IEnumerator LoadAsync(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var operation = SceneManager.LoadSceneAsync(sceneId.ToString(), mode);
            
            if (operation == null)
                throw new ArgumentException($"Scene {sceneId} not found in build scene list");
            
            yield return operation;
            
            Debug.Log($"Scene {sceneId} loaded.");
        }

        public IEnumerator UnloadAsync(SceneId sceneId, UnloadSceneOptions options = UnloadSceneOptions.None)
        {
            var operation = SceneManager.UnloadSceneAsync(sceneId.ToString(), options);
            
            if (operation == null)
                throw new ArgumentException($"Scene {sceneId} not found in build scene list");
            
            yield return operation;
            
            Debug.Log($"Scene {sceneId} unloaded.");
        }

        public void Load(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneId.ToString(), mode);
            Debug.Log($"Scene {sceneId} loaded.");
        }
    }
}
using System.Collections;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        IEnumerator UnloadAsync(string sceneName, UnloadSceneOptions options = UnloadSceneOptions.None);
    }
}
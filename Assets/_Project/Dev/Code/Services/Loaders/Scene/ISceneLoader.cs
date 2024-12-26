using System.Collections;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single);
        void Load(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single);
        IEnumerator UnloadAsync(SceneId sceneId, UnloadSceneOptions options = UnloadSceneOptions.None);
    }
}
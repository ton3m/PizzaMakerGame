using PizzaMaker.Code.Services.Loaders.Scene;
using UnityEngine;

namespace PizzaMaker.Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Launch Config", fileName = "GameLaunchConfig")]
    public class GameLaunchConfig : ScriptableObject
    {
        public SceneId StartScene = SceneId.Gameplay;
        public bool StartFromBootstrapScene = true;
    }
}
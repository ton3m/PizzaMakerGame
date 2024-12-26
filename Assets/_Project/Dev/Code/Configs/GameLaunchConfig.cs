using UnityEngine;

namespace PizzaMaker.Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Launch Config", fileName = "GameLaunchConfig")]
    public class GameLaunchConfig : ScriptableObject
    {
        public bool StartFromBootstrapScene = true;
    }
}
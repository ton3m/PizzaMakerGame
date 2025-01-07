using PizzaMaker.Code.Utils.ScenePicker;
using UnityEngine;

namespace PizzaMaker.Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Launch Config", fileName = "GameLaunchConfig")]
    public class GameLaunchConfig : ScriptableObject
    {
        [SerializeField] private ScenePickerField _targetScene;
        [SerializeField] private bool _startFromBootstrapScene = true;
        [SerializeField] private AddSceneToBuildButton _addButton;
        
        public string TargetScene => _targetScene.SelectedScene;
        public bool StartFromBootstrapScene => _startFromBootstrapScene;
    }
}
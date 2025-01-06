using UnityEditor;
using UnityEngine;

namespace PizzaMaker.Code.Configs
{
    public class BuildSettingsScenes
    {
        [MenuItem("Tools/Get Build Scenes")]
        public static void GetBuildScenes()
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    Debug.Log($"Scene: {scene.path}");
                }
            }
        }
    }
}
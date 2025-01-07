using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.ScenePicker;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Dev.Code.Utils.ScenePicker.Editor
{
    [CustomPropertyDrawer(typeof(AddSceneToBuildButton))]
    public class AddSceneToBuildPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string scenePath = SceneManager.GetActiveScene().path;

            // Check if the current scene is already in build settings
            bool isSceneAdded = EditorBuildSettings.scenes.Any(scene => scene.path == scenePath);

            // Show button only if the scene is not added
            if (!isSceneAdded)
            {
                string buttonText = $"Add \"{sceneName}\" scene"; // Button text with scene name
                float buttonWidth = GUI.skin.button.CalcSize(new GUIContent(buttonText)).x; // Calculate button width
                Rect buttonRect = new Rect(position.x + (position.width - buttonWidth) / 2, position.y, buttonWidth,
                    position.height * 1.5f); // Center button

                // Set tooltip for button
                GUIContent buttonContent =
                    new GUIContent(buttonText, "Adds current scene to Build Profile.");

                if (GUI.Button(buttonRect, buttonContent))
                {
                    List<EditorBuildSettingsScene> scenes =
                        new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes)
                        {
                            new EditorBuildSettingsScene(scenePath, true)
                        };
                    EditorBuildSettings.scenes = scenes.ToArray();
                    Debug.Log($"Added scene '{scenePath}' to build settings.");
                }
            }
        }
    }
}
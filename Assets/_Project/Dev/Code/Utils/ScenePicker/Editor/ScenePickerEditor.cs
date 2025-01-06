using PizzaMaker.Code.Utils.ScenePicker;
using UnityEditor;
using UnityEngine;

// Custom property drawer for ScenePickerField to handle the dropdown
namespace PizzaMaker.Dev.Code.Utils.ScenePicker.Editor
{
    [CustomPropertyDrawer(typeof(ScenePickerField))]
    public class ScenePickerFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Ensure "selectedScene" property exists
            SerializedProperty selectedSceneProperty = property.FindPropertyRelative(nameof(ScenePickerField.SelectedScene));
        
            if (selectedSceneProperty == null)
            {
                EditorGUI.HelpBox(position, "'selectedScene' property is missing or improperly configured.",
                    MessageType.Error);
                return;
            }

            // Get all the scenes in Build Settings
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            if (scenes.Length == 0)
            {
                // If no scenes are available, show a warning
                EditorGUI.HelpBox(position, "No scenes are added to Build Settings.", MessageType.Warning);
                return;
            }

            string[] sceneNames = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
            }

            // Get the current selected scene index
            int currentIndex = System.Array.IndexOf(sceneNames, selectedSceneProperty.stringValue);
            if (currentIndex == -1) currentIndex = 0;

            // Render the dropdown for scene selection
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);

            // Update the selected scene when the value changes
            if (newIndex >= 0 && newIndex < sceneNames.Length)
            {
                selectedSceneProperty.stringValue = sceneNames[newIndex];
            }
        }
    }
}
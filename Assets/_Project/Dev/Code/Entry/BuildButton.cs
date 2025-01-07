using UnityEditor;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    // ... existing code ...
    
    [ContextMenu("Add Scene to Build Settings")]
    private void AddCurrentSceneToBuildSettings()
    {
        string currentScenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;

        if (!string.IsNullOrEmpty(currentScenePath))
        {
            var buildScenes = EditorBuildSettings.scenes;
            foreach (var scene in buildScenes)
            {
                if (scene.path == currentScenePath)
                {
                    Debug.Log("Current scene is already in Build Settings.");
                    return;
                }
            }

            var newBuildScenes = new EditorBuildSettingsScene[buildScenes.Length + 1];
            buildScenes.CopyTo(newBuildScenes, 0);
            newBuildScenes[buildScenes.Length] = new EditorBuildSettingsScene(currentScenePath, true);

            EditorBuildSettings.scenes = newBuildScenes;
            Debug.Log("Added current scene to Build Settings.");
        }
        else
        {
            Debug.LogError("Current scene is not saved. Please save the scene first.");
        }
    }
}
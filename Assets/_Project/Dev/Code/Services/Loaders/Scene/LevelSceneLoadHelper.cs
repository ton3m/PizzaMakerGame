using System;

namespace PizzaMaker.Code.Services.Loaders.Scene
{
    public abstract class LevelSceneLoadHelper
    {
        public static SceneId GetSceneIdFor(int levelIndex)
        {
            SceneId sceneId = GetSceneIdBy(levelIndex);

            if (sceneId == SceneId.None)
                throw new ArgumentException($"No sceneId found for level index {levelIndex}");

            return sceneId;
        }

        public static bool IsValidIndex(int levelIndex) => 
            GetSceneIdBy(levelIndex) != SceneId.None;
        
        private static SceneId GetSceneIdBy(int levelIndex)
        {
            int sceneId = (int)SceneId.Level1 + levelIndex;

            if (Enum.IsDefined(typeof(SceneId), sceneId))
                return (SceneId)sceneId;

            return SceneId.None;
        }
    }
}
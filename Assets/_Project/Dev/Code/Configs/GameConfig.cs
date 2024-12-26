using System;
using PizzaMaker.Code.Services.Loaders.Scene;

namespace PizzaMaker.Code.Configs
{
    public class GameConfig 
    {
        public int NextLevelIndex => PassingLevelIndex + 1;
        public int PassingLevelIndex { get; private set; } = -1;
        
        public float Health { get; set; } = 100;

        public void SetLastLevel(int index)
        {
            if (LevelSceneLoadHelper.IsValidIndex(index) == false)
                throw new ArgumentException($"Provided index {index} is not a valid level index");
            
            PassingLevelIndex = index;
        }

        public void OnLevelPassed() => PassingLevelIndex++;
    }
}
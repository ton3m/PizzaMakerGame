using System;
using PizzaMaker.Code.Services.Loaders.Scene;

namespace PizzaMaker.Code.Services.StateSwitching.Level
{
    public class LevelLoadData
    {
        public readonly SceneId Scene;
        private readonly IDisposable _disposeLevel;

        public LevelLoadData(SceneId scene, IDisposable disposeLevel)
        {
            Scene = scene;
            _disposeLevel = disposeLevel;
        }

        public void DisposeLevel() => _disposeLevel.Dispose();
    }
}
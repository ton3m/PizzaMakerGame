using System;
using System.Collections;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Entry.Bootstraps.Main;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Utils.DI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker.Code.Services.StateSwitching.Level
{
    public class LevelSwitcher
    {
        private readonly ISceneLoader _sceneLoader;

        private LevelLoadData _currentLevelLoadData;

        private readonly Func<int> _getNextLevelIndex;
        private readonly Func<DIContainer> _createLevelContainer;

        public LevelSwitcher(DIContainer container)
        {
            _sceneLoader = container.Resolve<ISceneLoader>();

            _getNextLevelIndex = () => container.Resolve<GameConfig>().NextLevelIndex;
            _createLevelContainer = () => new DIContainer(container);
        }

        private bool HasLoadedLevel => _currentLevelLoadData != null;

        public IEnumerator EnterNextLevel()
        {
            int index = _getNextLevelIndex();

            if (HasLoadedLevel) yield return ExitCurrentLevel();

            yield return Enter(index);
        }

        private IEnumerator Enter(int levelIndex)
        {
            Debug.Log("Level bootstrap");
            SceneId sceneId = LevelSceneLoadHelper.GetSceneIdFor(levelIndex);

            DIContainer container = _createLevelContainer();

            LevelBootstrap bootstrap = new();

            var loadData = new LevelLoadData(sceneId, bootstrap);

            yield return _sceneLoader.LoadAsync(loadData.Scene, LoadSceneMode.Additive);
            yield return bootstrap.Run(container);

            _currentLevelLoadData = loadData;
        }

        public IEnumerator ExitCurrentLevel()
        {
            if (HasLoadedLevel == false)
                throw new Exception("No level loaded");

            _currentLevelLoadData.DisposeLevel();

            yield return _sceneLoader.UnloadAsync(_currentLevelLoadData.Scene);

            _currentLevelLoadData = null;
        }
    }
}
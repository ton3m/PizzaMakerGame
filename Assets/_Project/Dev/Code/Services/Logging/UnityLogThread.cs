using System;
using UnityEngine;

namespace PizzaMaker.Code.Services.Logging
{
    public class UnityLogThread : IThread, IActivatable
    {
        private static IThread _instance;
        private bool _enabled = true;

        public UnityLogThread()
        {
            _instance = this;
        }

        public static IThread Instance => _instance;
        
        public void SetActive(bool active) => _enabled = active;

        public static IThread NewChild(string id)
        {
            if (_instance == null)
                throw new NullReferenceException($"Can't create log thread child. {nameof(UnityLogThread)} was not instantiated.");

            return new ChildTread(id, _instance);
        }

        public void Write(object message, LogType type)
        {
            if (!_enabled) return;

            switch (type)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}
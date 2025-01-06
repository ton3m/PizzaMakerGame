using System;

namespace PizzaMaker.Code.Services.Logging
{
    public class Logger : ILogger
    {
        private bool _enabled = true;
        
        private readonly IThread _thread;

        public Logger(IThread thread) => _thread = thread;
        
        public ILogger SetActive(bool isActive)
        {
            _enabled = isActive;
            return this;
        }

        public void Log(object message) => Try(() => _thread.Write(message, LogType.Log));

        public void LogError(object message) => Try(() => _thread.Write(message, LogType.Error));

        public void LogWarning(object message) => Try(() => _thread.Write(message, LogType.Warning));

        private void Try(Action action)
        {
            if (_enabled) action();
        }
    }
}
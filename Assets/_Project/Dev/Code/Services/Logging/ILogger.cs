namespace PizzaMaker.Code.Services.Logging
{
    public interface ILogger 
    {
        public ILogger SetActive(bool isActive);

        public void Log(object message);
        public void LogError(object message);
        public void LogWarning(object message);
    }
}
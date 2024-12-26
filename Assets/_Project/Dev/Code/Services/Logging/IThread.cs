namespace PizzaMaker.Code.Services.Logging
{
    public interface IThread
    {
        public void Write(object message, LogType type);
    }
}
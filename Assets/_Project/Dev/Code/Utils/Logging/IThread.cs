namespace PizzaMaker.Code.Utils.Logging
{
    public interface IThread
    {
        public void Write(object message, LogType type);
    }
}
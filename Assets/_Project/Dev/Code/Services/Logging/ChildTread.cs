namespace PizzaMaker.Code.Services.Logging
{
    public class ChildTread : IThread
    {
        private readonly string _id;
        private readonly IThread _parent;

        public ChildTread(string id, IThread parent)
        {
            _id = id;
            _parent = parent;
        }

        public void Write(object message, LogType type)
        {
            message = _id + ": " + message;
            _parent.Write(message, type);
        }
    }
}
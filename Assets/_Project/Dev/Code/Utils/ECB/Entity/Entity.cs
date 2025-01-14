namespace PizzaMaker.Code.Utils.ECB
{
    public class Entity : IEntity
    {
        public Components Components { get; } = new();
        public Behaviours Behaviours { get; } = new();
    }
}
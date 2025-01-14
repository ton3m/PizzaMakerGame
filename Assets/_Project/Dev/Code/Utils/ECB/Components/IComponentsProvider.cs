namespace PizzaMaker.Code.Utils.ECB
{
    public interface IComponentsProvider
    {
        T Get<T>();
    }
}
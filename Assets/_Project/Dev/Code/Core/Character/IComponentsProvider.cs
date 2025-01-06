namespace PizzaMaker.Code.Core.Character
{
    public interface IComponentsProvider
    {
        T GetComponent<T>();
    }
}
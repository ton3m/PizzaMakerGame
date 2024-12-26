namespace PizzaMaker.Code.Utils.Reactive.Main.Abstraction
{
    public interface IObserver<T>
    {
        void Notify(T previous, T current);
    }
    
    public interface ISingleObserver<T>
    {
        void Notify(T value);
    }
}
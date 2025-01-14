namespace PizzaMaker.Code.Utils.Reactive
{
    public interface IObserver<T>
    {
        void Notify(T value);
    }
    
    public interface ISingleObserver<T>
    {
        void Notify(T value);
    }

    public interface IObserver
    {
        void Notify(); 
    }
}
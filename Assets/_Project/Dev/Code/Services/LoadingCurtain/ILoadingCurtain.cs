namespace PizzaMaker.Code.Services.LoadingCurtain
{
    public interface ILoadingCurtain
    {
        public bool IsActive { get; }
        
        void Show();
        void Hide();
    }
}
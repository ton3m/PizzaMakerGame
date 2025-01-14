namespace PizzaMaker.Code.Services.UI.Windows
{
    public interface IWindowsService
    {
        void Open(WindowId id);
        void CloseAll();
    }
}
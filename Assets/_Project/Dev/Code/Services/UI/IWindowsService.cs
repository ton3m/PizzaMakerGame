using PizzaMaker.Code.UI.Windows;

namespace PizzaMaker.Code.Services.UI
{
    public interface IWindowsService
    {
        void Open(WindowId id);
        void CloseAll();
    }
}
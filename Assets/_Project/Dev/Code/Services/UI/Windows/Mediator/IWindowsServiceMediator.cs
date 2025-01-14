namespace PizzaMaker.Code.Services.UI.Windows
{
    public interface IWindowsServiceMediator
    {
        void RequestOpen(object sender, WindowId windowId);
    }
}
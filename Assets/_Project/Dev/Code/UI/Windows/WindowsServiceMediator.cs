using PizzaMaker.Code.Services.Logging;
using PizzaMaker.Code.Services.UI;

namespace PizzaMaker.Code.UI.Windows
{
    public class WindowsServiceMediator : IWindowsServiceMediator
    {
        private readonly ILogger _logger = 
            new Logger(UnityLogThread.NewChild(nameof(WindowsServiceMediator)));
        
        private readonly IWindowsService _windowsService;

        public WindowsServiceMediator(IWindowsService windowsService)
        {
            _windowsService = windowsService;
        }
        
        public void RequestOpen(object sender, WindowId windowId)
        {
            _logger.Log($"Object {sender.ToString()} requested open window {windowId}");
            
            _windowsService.Open(windowId);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PizzaMaker.Code.Services.UI.Windows
{
    public class WindowsHolder : MonoBehaviour
    {
        [SerializeField] private List<IdentifiedWindow> _windows;
    
        public Dictionary<WindowId, GameObject> Windows =>
            _windows.ToDictionary(w => w.Id, w => w.Window);
    }
}
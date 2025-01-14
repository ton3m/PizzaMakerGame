using System.Collections.Generic;
using PizzaMaker.Code.Services.UI.Layers;
using PizzaMaker.Code.Services.UI.Windows;
using UnityEngine;

namespace PizzaMaker.Code.Services.UI.Root
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private LayersHolder _layersHolder;
        [SerializeField] private WindowsHolder _windowsHolder;

        public Dictionary<WindowId, GameObject> Windows => _windowsHolder.Windows;
        public Dictionary<LayerId, GameObject[]> Layers => _layersHolder.Layers;
    }
}
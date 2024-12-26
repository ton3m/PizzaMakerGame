using System.Collections.Generic;
using PizzaMaker.Code.UI.Layers;
using PizzaMaker.Code.UI.Windows;
using UnityEngine;

namespace PizzaMaker.Code.UI.Root
{
    public class UIHolder : MonoBehaviour
    {
        [SerializeField] private LayersHolder _layersHolder;
        [SerializeField] private WindowsHolder _windowsHolder;

        public Dictionary<WindowId, GameObject> Windows => _windowsHolder.Windows;
        public Dictionary<LayerId, GameObject[]> Layers => _layersHolder.Layers;
    }
}
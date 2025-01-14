using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PizzaMaker.Code.Services.UI.Layers
{
    public class LayersActivator : ILayersActivator
    {
        private readonly Dictionary<LayerId, GameObject[]> _layers;

        public LayersActivator(Dictionary<LayerId, GameObject[]> layers)
        {
            _layers = layers;
        }

        public void Enable(LayerId layerId)
        {
            if (!_layers.TryGetValue(layerId, out GameObject[] layer))
                throw new ArgumentException($"Layer {layerId} not found");

            foreach (GameObject obj in layer) 
                obj.SetActive(true);
        }

        public void Disable(LayerId layerId)
        {
            if (!_layers.TryGetValue(layerId, out GameObject[] layer))
                throw new ArgumentException($"Layer {layerId} not found");

            foreach (GameObject obj in layer)
                obj.SetActive(false);
        }

        public void DisableAll() => _layers.Keys.ToList().ForEach(Disable);
    }
}
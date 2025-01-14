using System;
using UnityEngine;

namespace PizzaMaker.Code.Services.UI.Layers
{
    [Serializable]
    public struct IdentifiedLayer
    {
        public LayerId Id;
        public GameObject[] Elements;
    }
}
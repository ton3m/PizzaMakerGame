using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker.Code.Core.SnakeMovement
{
    public struct SnakeTransformsData
    {
        public readonly List<Transform> Segments;
        public Transform Head;

        public SnakeTransformsData(List<Transform> segments, Transform head)
        {
            Segments = segments;
            Head = head;
        }
    }
}
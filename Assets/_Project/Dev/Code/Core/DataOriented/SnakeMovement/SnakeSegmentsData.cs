using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker.Code.Core.SnakeMovement
{
    public struct SnakeSegmentsData
    {
        public readonly List<Transform> Segments;
        public Transform Head;

        public SnakeSegmentsData(List<Transform> segments, Transform head)
        {
            Segments = segments;
            Head = head;
        }
    }
}
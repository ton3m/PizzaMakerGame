using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.ECB;
using UnityEngine;
using Common_IUpdatable = PizzaMaker.Code.Utils.Common.IUpdatable;

namespace PizzaMaker.Code.Core.SnakeMovement
{
    public class SnakeMoveBehaviour : Common_IUpdatable, IComponentsInit
    {
        private SnakeMoveData data;
        private SnakeSegmentsData segmentsData;

        public void Initialize(IComponentsProvider components)
        {
            segmentsData = components.Get<SnakeSegmentsData>();
            data = components.Get<SnakeMoveData>();
        }

        public void Update()
        {
            var segments = segmentsData.Segments.ToList();
            
            if (segments.Count == 0) return;
            
            if (segmentsData.Head != null) HandleHeadFor(segments);
            
            for (int i = 1; i < segments.Count; i++)
            {
                var previousPart = segments[i - 1];
                var currentPart = segments[i];
                
                if (previousPart == null || currentPart == null) continue;
                
                var sideTime = Time.deltaTime * data.SideFollowSpeed;
                var forwardTime = Time.deltaTime * data.ForwardFollowSpeed;
                
                var targetZ = previousPart.position.z - data.ForwardFollowDistance;
                var targetX = previousPart.position.x;
                
                var x = Mathf.Lerp(currentPart.position.x, targetX, sideTime);
                var z = Mathf.Lerp(currentPart.position.z, targetZ, forwardTime);
                var y = previousPart.position.y;
                
                currentPart.position = new Vector3(x, y, z);
            }
        }

        private void HandleHeadFor(List<Transform> segments)
        {
            if (data.HasHeadOffset)
                segments.Insert(0, segmentsData.Head);
            else
                segments[0].position = segmentsData.Head.position;
        }
    }
}
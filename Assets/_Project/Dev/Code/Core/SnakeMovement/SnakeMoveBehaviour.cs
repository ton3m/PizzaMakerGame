using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services;
using UnityEngine;

namespace PizzaMaker.Code.Core.SnakeMovement
{
    public class SnakeMoveBehaviour : IUpdatable, IInitBehaviour
    {
        private SnakeMoveConfig _config;
        private SnakeTransformsData transformsData;

        public void Initialize(IComponentsProvider entity)
        {
            transformsData = entity.GetComponent<SnakeTransformsData>();
            _config = entity.GetComponent<SnakeMoveConfig>();
        }

        public void Update()
        {
            var segments = transformsData.Segments.ToList();
            
            if (segments.Count == 0) return;
            
            if (transformsData.Head != null) HandleHeadFor(segments);
            
            for (int i = 1; i < segments.Count; i++)
            {
                var previousPart = segments[i - 1];
                var currentPart = segments[i];
                
                if (previousPart == null || currentPart == null) continue;
                
                var sideTime = Time.deltaTime * _config.SideFollowSpeed;
                var forwardTime = Time.deltaTime * _config.ForwardFollowSpeed;
                
                var targetZ = previousPart.position.z - _config.ForwardFollowDistance;
                var targetX = previousPart.position.x;
                
                var x = Mathf.Lerp(currentPart.position.x, targetX, sideTime);
                var z = Mathf.Lerp(currentPart.position.z, targetZ, forwardTime);
                var y = previousPart.position.y;
                
                currentPart.position = new Vector3(x, y, z);
            }
        }

        private void HandleHeadFor(List<Transform> segments)
        {
            if (_config.HasHeadOffset)
                segments.Insert(0, transformsData.Head);
            else
                segments[0].position = transformsData.Head.position;
        }
    }
}
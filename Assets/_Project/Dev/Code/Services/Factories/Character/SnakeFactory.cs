using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Core.Collision;
using UnityEngine;

namespace PizzaMaker.Code.Services.Factories.Character
{
    public class SnakeFactory
    {
        public List<Transform> CreateSnakeSegments(Vector3 position, GameObject prefab, Transform parent = null)
        {
            var doughs =
                FactoryExtensions
                    .DoMultiple(10, () => 
                        Object.Instantiate(prefab, position, Quaternion.identity))
                    .SetParent(parent);

            var segments = doughs.Select(x => x.transform).ToList();

            SetupSnakeSegments(segments);

            return segments;
        }

        private void SetupSnakeSegments(List<Transform> segments)
        {
            var colliders = segments
                .Select(x => x.gameObject.AddComponent<SphereCollider>())
                .ToList();

            var rigidBodies = segments
                .Select(x => x.gameObject.AddComponent<Rigidbody>())
                .ToList();

            segments.ForEach(x => x.gameObject.AddComponent<CollisionDetector>());

            colliders.ForEach(x =>
            {
                x.radius = 1f;
                x.isTrigger = true;
            });

            rigidBodies.ForEach(x =>
            {
                x.useGravity = false;
                x.isKinematic = true;
            });
        }
    }
}
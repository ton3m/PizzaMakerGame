using System.Collections.Generic;
using PizzaMaker.Code.Core.Collision;

namespace PizzaMaker.Code.Core.SnakeCollision
{
    public struct SnakeCollisionData
    {
        public readonly List<ICollisionDetector> CollisionDetectors;

        public SnakeCollisionData(List<ICollisionDetector> collisionDetectors)
        {
            CollisionDetectors = collisionDetectors;
        }
    }
}
using System;
using System.Collections.Generic;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.Ingredients;
using PizzaMaker.Code.Utils.ECB;
using PizzaMaker.Code.Utils.Reactive;
using UnityEngine;

namespace PizzaMaker.Code.Core.SnakeCollision
{
    public class SnakeCollisionHandler : IComponentsInit, IDisposable
    {
        private List<ICollisionDetector> _collisionDetectors;
        private List<IDisposable> _disposables = new();
        
        public void Initialize(IComponentsProvider components)
        {
            _collisionDetectors = components
                .Get<SnakeCollisionData>().CollisionDetectors;
         
            for (int i = 0; i < _collisionDetectors.Count; i++)
            {
                var collisionDetector = _collisionDetectors[i];

                var index = i;
                
                collisionDetector.TriggerEntered
                    .Subscribe(c => OnTriggerEntered(c, index))
                    .DisposeIn(_disposables);
            }
        }

        public void Dispose() => 
            _disposables.ForEach(x => x.Dispose());

        private void OnTriggerEntered(Collider collider, int index)
        {
            if (collider.TryGetComponent<Ingredient>(out Ingredient ingredient))
            {
                Debug.Log($"Ingredient {ingredient.Id} entered segment {index}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.Ingredients;
using UnityEngine;
using PizzaMaker.Code.Utils.Reactive;
namespace PizzaMaker.Code.Core.SnakeMovement
{
    public class SnakeCollisionHandler : IInitBehaviour, IDisposable
    {
        private List<ICollisionDetector> _collisionDetectors;
        private List<IDisposable> _disposables = new();
        
        public void Initialize(IComponentsProvider entity)
        {
            _collisionDetectors = entity
                .GetComponent<SnakeCollisionData>().CollisionDetectors;
         
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
using System;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Ingredients;
using PizzaMaker.Code.Utils.ECB;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using UnityEngine;

namespace PizzaMaker.Code.Core.DoughScaler
{
    public class DoughCollisionHandlerScaler : IComponentsInit, IDisposable
    {
        private readonly IDisposer _disposer = new Disposer();
        private Transform _scalable;

        public void Initialize(IComponentsProvider components)
        {
            components.Get<FinishCollisionDetector>()
                .CollisionDetector.TriggerEntered
                .Subscribe(OnCollision)
                .DisposeIn(_disposer);

            _scalable = components.Get<Scalable>().Transform;
        }

        private void OnCollision(Collider collider)
        {
            if (collider.TryGetComponent(out Ingredient ingredient) == false)
                return;
            
            var ingredientId = ingredient.Id;
            
            UnityEngine.Object.Destroy(ingredient.gameObject);
            
            Debug.Log(ingredientId);

            _scalable.localScale *= 1.3f;
        }

        public void Dispose() => _disposer.Dispose();
    }
}
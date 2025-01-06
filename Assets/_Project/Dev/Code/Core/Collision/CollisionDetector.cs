using PizzaMaker.Code.Utils.Reactive.Main;
using PizzaMaker.Code.Utils.Reactive.Main.Abstraction;
using UnityEngine;

namespace PizzaMaker.Code.Core.Collision
{
    public class CollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private readonly Subject<Collider> triggerEntered = new();
        private readonly Subject<Collider> triggerLeft = new();

        private readonly Subject<UnityEngine.Collision> _collisionEntered = new();
        private readonly Subject<UnityEngine.Collision> _collisionLeft = new();

        private void OnTriggerEnter(Collider other) =>
            triggerEntered.Notify(null, other);

        private void OnTriggerExit(Collider other) =>
            triggerLeft.Notify(null, other);

        private void OnCollisionEnter(UnityEngine.Collision collision) =>
            _collisionEntered.Notify(null, collision);
        
        private void OnCollisionExit(UnityEngine.Collision collision) =>
            _collisionLeft.Notify(null, collision);

        public IObservable<Collider> TriggerEntered => triggerEntered;
        public IObservable<Collider> TriggerLeft => triggerLeft;
        
        public IObservable<UnityEngine.Collision> CollisionEntered => _collisionEntered;
        public IObservable<UnityEngine.Collision> CollisionLeft => _collisionLeft;
    }
}
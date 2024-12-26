using PizzaMaker.Code.Utils.Reactive.Main;
using UnityEngine;

namespace PizzaMaker.Code.Core.Collision
{
    public class CollisionDetector : MonoBehaviour, ICollisionDetector
    {
        private readonly Subject<Collider> _enter = new();
        private readonly Subject<Collider> _exit = new();
        
        private void OnTriggerEnter(Collider other) => _enter.Notify(null, other);

        private void OnTriggerExit(Collider other) => _exit.Notify(null, other);

        public Utils.Reactive.Main.Abstraction.IObservable<Collider> Enter => _enter;
        public Utils.Reactive.Main.Abstraction.IObservable<Collider> Exit => _exit;
    }
}
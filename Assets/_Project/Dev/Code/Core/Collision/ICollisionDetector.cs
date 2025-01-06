using PizzaMaker.Code.Utils.Reactive.Main.Abstraction;
using UnityEngine;

namespace PizzaMaker.Code.Core.Collision
{
    public interface ICollisionDetector
    {
        IObservable<Collider> TriggerEntered { get; }
        IObservable<Collider> TriggerLeft { get; }
        
        IObservable<UnityEngine.Collision> CollisionEntered { get; }
        IObservable<UnityEngine.Collision> CollisionLeft { get; }
    }
}
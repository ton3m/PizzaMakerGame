using PizzaMaker.Code.Utils.Reactive.Main.Abstraction;
using UnityEngine;

namespace PizzaMaker.Code.Core.Collision
{
    public interface ICollisionDetector
    {
        IObservable<Collider> Enter { get; }
        IObservable<Collider> Exit { get; }
    }
}
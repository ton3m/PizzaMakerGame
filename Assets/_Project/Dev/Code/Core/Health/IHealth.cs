using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Core.Health
{
    public interface IHealth
    {
        IReadOnlyReactive<float> HP { get; }
        IReadOnlyReactive<bool> IsDead { get; }
        float MaxValue { get; }
        void ApplyDamage(float amount);
        void Heal(int amount);
    }
}
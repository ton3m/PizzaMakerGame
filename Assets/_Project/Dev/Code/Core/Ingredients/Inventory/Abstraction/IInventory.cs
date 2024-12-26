using System;

namespace PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction
{
    public interface IInventory
    {
        event Action<IngredientId> IngredientAdded;
        int Count { get; }
        void Add(IngredientId ingredient);
    }
}
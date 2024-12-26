using System;
using System.Collections.Generic;
using PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction;
using UnityEngine;

namespace PizzaMaker.Code.Core.Ingredients.Inventory
{
    public class Inventory : IInventory
    {
        public event Action<IngredientId> IngredientAdded;
        
        private readonly List<IngredientId> _ingredients = new();

        public int Count => _ingredients.Count;

        public void Add(IngredientId id)
        {
            Debug.Log("Ingredient added: " + id);
            _ingredients.Add(id);

            IngredientAdded?.Invoke(id);
        }
    }
}
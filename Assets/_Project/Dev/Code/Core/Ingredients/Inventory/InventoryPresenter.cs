using System;
using PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction;

namespace PizzaMaker.Code.Core.Ingredients.Inventory
{
    public class InventoryPresenter : IDisposable
    {
        private IInventory _inventory;
        private IInventoryView _inventoryView;

        public InventoryPresenter(IInventory inventory, IInventoryView view)
        {
            _inventory = inventory;
            _inventoryView = view;
            
            _inventory.IngredientAdded += OnIngredientAdded;
        }

        public void Dispose() =>
            _inventory.IngredientAdded -= OnIngredientAdded;

        private void OnIngredientAdded(IngredientId id) => 
            _inventoryView.Add(id, _inventory.Count);
    }
}
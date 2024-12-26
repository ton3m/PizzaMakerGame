using PizzaMaker.Code.Core.Ingredients.Inventory;
using PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction;

namespace PizzaMaker.Code.Core.Ingredients
{
    public class IngredientsCollector : IIngredientsCollector
    {
        IInventory _inventory;
        private readonly IViewsPull _viewsPull;

        public IngredientsCollector(IInventory inventory, IViewsPull viewsPull)
        {
            _inventory = inventory;
            _viewsPull = viewsPull;
        }

        public void Collect(Ingredient ingredient)
        {
            _viewsPull.Add(ingredient);
            _inventory.Add(ingredient.Id);
        }
    }
}
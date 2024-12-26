namespace PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction
{
    public interface IInventoryView
    {
        void Add(IngredientId id, int count);
    }
}
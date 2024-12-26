using PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction;
using UnityEngine;

namespace PizzaMaker.Code.Core.Ingredients.Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _attachPoint;
     
        private ViewsPull _viewsPull;

        public void Init(ViewsPull viewsPull)
        {
            _viewsPull = viewsPull;
        }
        
        public void Add(IngredientId id, int count)
        {
            GameObject ingredient = _viewsPull.Get(id);
            
            ingredient.transform.parent = _attachPoint;

            var scale = ingredient.transform.localScale;
            
            scale.x *= 2;
            scale.z *= 2;
            scale.y = 0.1f;
            
            ingredient.transform.localScale = scale;
            
            float height = (count-1) * 0.5f;
            ingredient.transform.position = _attachPoint.position + Vector3.up * height;

            ingredient.gameObject.SetActive(true);
        }
    }
}
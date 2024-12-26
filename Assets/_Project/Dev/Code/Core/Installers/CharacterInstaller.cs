using PathCreation;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.Health;
using PizzaMaker.Code.Core.Ingredients;
using PizzaMaker.Code.Core.Ingredients.Inventory;
using PizzaMaker.Code.Core.Ingredients.Inventory.Abstraction;
using PizzaMaker.Code.Core.Movement;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using UnityEngine;

namespace PizzaMaker.Code.Core.Installers
{
    public class CharacterInstaller : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PathFollower _pathFollower;
        [SerializeField] private SideMovement _sideMovement;
        [SerializeField] private CollisionDetector _collisionDetector;

        private IHealth _health;
        private ICharacterMovement _characterMovement;
        private IIngredientsCollector _ingredientsCollector;
        
        private readonly IDisposer _disposer = new Disposer();

        public CharacterInstaller Init(IHealth health, PathCreator pathCreator)
        {
            _health = health;
            
            _pathFollower.Init(pathCreator);
            
            _characterMovement = new CharacterMovement(_pathFollower, _sideMovement);

            InitInventory();

            _collisionDetector.Enter.Subscribe(HandleCollision).DisposeIn(_disposer);

            return this;
        }

        private void InitInventory()
        {
            IInventory inventory = new Inventory();
            ViewsPull viewsPull = new ViewsPull();

            _inventoryView.Init(viewsPull);

            _ingredientsCollector = new IngredientsCollector(inventory, viewsPull);
            
            new InventoryPresenter(inventory, _inventoryView).DisposeIn(_disposer);
        }
        
        public IHealth Health => _health;
        
        public ICollisionDetector CollisionDetector => _collisionDetector;
      
        private void OnDestroy() => _disposer.Dispose();

        public void Enable() => _characterMovement.Enable();

        public void Disable() => _characterMovement.Disable();
        
        private void HandleCollision(Collider obj)
        {
            if (obj.TryGetComponent(out Ingredient ingredient))
            {
                Debug.Log(ingredient.gameObject.activeSelf);
                _ingredientsCollector.Collect(ingredient);
            }
        }
    }
}
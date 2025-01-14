using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Utils.ECB;
using UnityEngine;
using Common_IUpdatable = PizzaMaker.Code.Utils.Common.IUpdatable;

namespace PizzaMaker.Code.Core.ForwardMovement
{
    public class ForwardMoveBehaviour : IComponentsInit, Common_IUpdatable
    {
        private Transform _body;
        private ForwardMoveSpeed _speed;
        
        public void Initialize(IComponentsProvider components)
        {
            _body = components.Get<CharacterStructureData>().Body;
            _speed = components.Get<ForwardMoveSpeed>();
        }
        
        public void Update()
        {
            Vector3 translation = _body.forward * (_speed.Value * Time.deltaTime);
            
            _body.Translate(translation);
        }
    }
}
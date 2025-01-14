using System;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services.Input;
using PizzaMaker.Code.Utils.ECB;
using UnityEngine;
using Common_IUpdatable = PizzaMaker.Code.Utils.Common.IUpdatable;

namespace PizzaMaker.Code.Core.SideMovement
{
    public class SideMoveBehaviour : Common_IUpdatable, IComponentsInit
    {
        private Transform _origin;
        private Transform _body;
    
        private SideMoveData data;
        private ISideDragInput _input;

        public void Initialize(IComponentsProvider components)
        {
            data = components.Get<SideMoveData>();
            _input = components.Get<ISideDragInput>();
            
            _body = components.Get<CharacterStructureData>().Body;
            _origin = components.Get<CharacterStructureData>().Origin;
        }

        public void Update()
        {
            if (_body == null || _origin == null)
                throw new NullReferenceException();
        
            Move(_input.Value);
        }

        private void Move(float input)
        {
            if (Mathf.Abs(input) < 0.1f) return;

            Vector3 current = _origin.InverseTransformPoint(_body.position);

            float targetX = input * data.Limit;
            float currentX = current.x;
            float deltaX = targetX - currentX;

            currentX += deltaX * Time.deltaTime * data.Speed;
            currentX = Mathf.Clamp(currentX, -data.Limit, data.Limit);

            current.x = currentX;

            _body.transform.position = _origin.TransformPoint(current);
        }
    }
}
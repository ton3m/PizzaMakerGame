using System;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services;
using PizzaMaker.Code.Services.Input;
using UnityEngine;

namespace PizzaMaker.Code.Core.SideMovement
{
    public class SideMoveBehaviour : IUpdatable, IInitBehaviour
    {
        private Transform _origin;
        private Transform _body;
    
        private SideMoveConfig _config;
        private ISideDragInput _input;

        public void Initialize(IComponentsProvider entity)
        {
            _config = entity.GetComponent<SideMoveConfig>();
            _input = entity.GetComponent<ISideDragInput>();
            
            _body = entity.GetComponent<CharacterStructureData>().Body;
            _origin = entity.GetComponent<CharacterStructureData>().Origin;
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

            float targetX = input * _config.Limit;
            float currentX = current.x;
            float deltaX = targetX - currentX;

            currentX += deltaX * Time.deltaTime * _config.Speed;
            currentX = Mathf.Clamp(currentX, -_config.Limit, _config.Limit);

            current.x = currentX;

            _body.transform.position = _origin.TransformPoint(current);
        }
    }
}
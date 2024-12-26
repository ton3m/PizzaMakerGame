using UnityEngine;

namespace PizzaMaker.Code.Core.Movement
{
    public class SideMovement : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _body;

        [SerializeField] private float _speed = 5;
        [SerializeField] private float _limit = 3;

        private void Update()
        {
            float input = GetMouseInput();

            Move(input);
        }
        
        private void Move(float input)
        {
            if (Mathf.Abs(input) < 0.1f) return;

            Vector3 current = _origin.InverseTransformPoint(_body.position);
            
            float targetX = input * _limit;
            float currentX = current.x;
            float deltaX = targetX - currentX;
            
            currentX += deltaX * Time.deltaTime * _speed;
            currentX = Mathf.Clamp(currentX, -_limit, _limit);
            
            current.x = currentX;
            
            _body.transform.position = _origin.TransformPoint(current);
        }
        
        private float GetMouseInput()
        {
            if (UnityEngine.Input.GetMouseButton(0) == false)
                return 0;

            float screenHalfWidth = Screen.width * 0.5f;

            float percentageX = ((UnityEngine.Input.mousePosition.x - screenHalfWidth) / screenHalfWidth) * 2;

            percentageX = Mathf.Clamp(percentageX, -1.0f, 1.0f);

            return percentageX;
        }

        private float Input()
        {
            float input = 0;

            if (UnityEngine.Input.GetKey(KeyCode.A))
                input = -1;
            else if (UnityEngine.Input.GetKey(KeyCode.D))
                input = 1;

            return input;
        }
    }
}
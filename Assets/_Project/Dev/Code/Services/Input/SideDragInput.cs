using UnityEngine;

namespace PizzaMaker.Code.Services.Input
{
    public class SideDragInput : ISideDragInput
    {
        public float Value => GetMouseInput() + GetKeyboardInput();

        private float GetMouseInput()
        {
            if (UnityEngine.Input.GetMouseButton(0) == false)
                return 0;

            float screenHalfWidth = Screen.width * 0.5f;

            float percentageX = ((UnityEngine.Input.mousePosition.x - screenHalfWidth) / screenHalfWidth) * 2;

            percentageX = Mathf.Clamp(percentageX, -1.0f, 1.0f);

            return percentageX;
        }

        private float GetKeyboardInput()
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
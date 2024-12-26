using UnityEngine;

namespace PizzaMaker.Code.UI.Buttons
{
    public class DisableButton : OnClickedButton
    {
        [SerializeField] private GameObject _targetObject;

        protected override void OnClicked() => _targetObject.SetActive(false);
    }
}
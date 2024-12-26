using DG.Tweening;
using UnityEngine;

namespace PizzaMaker.LegacyCode.UI
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private float delay;

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1).SetDelay(delay);
        }
    }
}

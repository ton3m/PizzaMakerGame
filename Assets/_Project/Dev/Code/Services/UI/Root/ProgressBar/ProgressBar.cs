using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PizzaMaker.Code.Services.UI.Root.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _levelText;

        public void SetLevel(int value) => _levelText.text = value.ToString();
        public void SetProgress(float value) => _progressSlider.value = value;
    }
}
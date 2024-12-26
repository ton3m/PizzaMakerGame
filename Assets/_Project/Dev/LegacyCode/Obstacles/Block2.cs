using System.Linq;
using TMPro;
using UnityEngine;

namespace PizzaMaker.LegacyCode.Obstacles
{
    public class Block2 : MonoBehaviour
    {
        [SerializeField] private GameObject _complete;
        [SerializeField] private GameObject _broken;
        [SerializeField] private MeshRenderer _mesh;

        [SerializeField] private TextMeshPro _text;
    
        [SerializeField] private int _size = 10;
    
        public float Size => _size;

        private void Start()
        {
            _complete.SetActive(true);
            _broken.SetActive(false);
            _text.text = _size.ToString();
        }

        public void Break()
        {
            _complete.SetActive(false);
            _broken.SetActive(true);

            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            renderers.ToList().ForEach(r => r.material = _text.material);
        }
    }
}
using UnityEngine;

namespace PizzaMaker.Code.Core.Character
{
    public class CharacterStructure : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _body;
        
        public Transform Origin => _origin;
        public Transform Body => _body;
    }
}
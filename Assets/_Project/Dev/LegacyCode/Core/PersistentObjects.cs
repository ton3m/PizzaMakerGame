using UnityEngine;

namespace PizzaMaker.LegacyCode.Core
{
    public class PersistentObjects : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

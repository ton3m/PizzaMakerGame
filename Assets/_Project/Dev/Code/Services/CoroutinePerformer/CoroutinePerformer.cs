using System.Collections;
using UnityEngine;

namespace PizzaMaker.Code.Services.CoroutinePerformer
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake() => DontDestroyOnLoad(this);

        public Coroutine StartPerform(IEnumerator routine) => 
            StartCoroutine(routine);

        public void StopPerform(Coroutine routine) => 
            StopCoroutine(routine);
    }
}
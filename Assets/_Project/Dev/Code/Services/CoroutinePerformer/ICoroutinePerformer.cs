using System.Collections;
using UnityEngine;

namespace PizzaMaker.Code.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerform(IEnumerator routine);
        
        void StopPerform(Coroutine routine);
    }
}
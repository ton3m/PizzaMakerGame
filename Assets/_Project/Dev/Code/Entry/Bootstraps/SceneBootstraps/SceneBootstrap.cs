using System.Collections;
using PizzaMaker.Code.Utils.DI;
using UnityEngine;

namespace PizzaMaker.Code.Entry.Bootstraps.SceneBootstraps
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract IEnumerator Run(DIContainer container);
    }
}
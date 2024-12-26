using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker.LegacyCode.Core.Game
{
    public class LoadingManager : MonoBehaviour
    {
        /*[SerializeField] private GameObject*/
        private void Awake()
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LoadingLevel", 1));
        }
    }
}

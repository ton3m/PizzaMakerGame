using PizzaMaker.LegacyCode.Core.Game;
using UnityEngine;

namespace PizzaMaker.LegacyCode.UI
{
    public class StartGame : MonoBehaviour
    {
        public void GameStart()
        {
            GameManager.Instance.gameStarted = true;
        }
    }
}

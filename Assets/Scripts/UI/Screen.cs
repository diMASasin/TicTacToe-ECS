using UnityEngine;

namespace TicTacToe
{
    public class Screen : MonoBehaviour
    {
        public void Show(bool state)
        {
            gameObject.SetActive(true);
        }
    }
}
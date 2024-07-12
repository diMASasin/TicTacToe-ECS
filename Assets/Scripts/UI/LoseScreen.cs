using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TicTacToe
{
    public class LoseScreen : Screen
    {
        [SerializeField] private Button _button;

        private void OnEnable() => _button.onClick.AddListener(OnRestartClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnRestartClick);

        public void OnRestartClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
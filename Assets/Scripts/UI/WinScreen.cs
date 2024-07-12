using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TicTacToe
{
    public class WinScreen : Screen
    {
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;

        private void OnEnable() => _button.onClick.AddListener(OnRestartClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnRestartClick);

        public void SetWinner(SignType value)
        {
            _text.text = value switch
            {
                SignType.Cross => "КРЕСТИК",
                SignType.Zero => "НОЛИК",
                SignType.None => throw new ArgumentOutOfRangeException(nameof(value), value, null),
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        public void OnRestartClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
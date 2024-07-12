using System;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        public void SetTurn(SignType value)
        {
            _text.text = value switch
            {
                SignType.Cross => "X",
                SignType.Zero => "0",
                SignType.None => throw new ArgumentOutOfRangeException(nameof(value), value, null),
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}
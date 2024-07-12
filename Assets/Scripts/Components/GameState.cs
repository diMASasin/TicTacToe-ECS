using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class GameState
    {
        public SignType CurrentType = SignType.Zero;
        public readonly Dictionary<Vector2Int, int> Cells = new();
    }
}
using UnityEngine;

namespace TicTacToe.Configurations
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        public int LevelWidth = 3;
        public int LevelHeight = 3;
        public int ChainLength = 3;
        public CellView CellView;
        public Vector2 Offset;
        public SignView CrossView;
        public SignView ZeroView;
    }
}
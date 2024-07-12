using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace TicTacToe.Systems
{
    public static class GameExtensions
    {
        public static int GetLongestChain(this Dictionary<Vector2Int, int> cells, Vector2Int position, EcsPool<Taken> takenPool)
        {
            int startEntity = cells[position];
            
            if (takenPool.Has(startEntity) == false)
                return 0;
            
            SignType startType = takenPool.Get(startEntity).Value;
            int horizontalLength = 1;
            int verticalLength = 1;
            int diagonalLength = 1;
            int secondDiagonalLength = 1;

            Vector2Int direction = new Vector2Int(-1, 0);
            GetLength(cells, takenPool, position, startType, ref horizontalLength, direction);
            direction = new Vector2Int(1, 0);
            GetLength(cells, takenPool, position, startType, ref horizontalLength, direction);
            
            direction = new Vector2Int(0, -1);
            GetLength(cells, takenPool, position, startType, ref verticalLength, direction);
            direction = new Vector2Int(0, 1);
            GetLength(cells, takenPool, position, startType, ref verticalLength, direction);
            
            direction = new Vector2Int(1, 1);
            GetLength(cells, takenPool, position, startType, ref diagonalLength, direction);
            direction = new Vector2Int(-1, -1);
            GetLength(cells, takenPool, position, startType, ref diagonalLength, direction);
            
            direction = new Vector2Int(-1, 1);
            GetLength(cells, takenPool, position, startType, ref secondDiagonalLength, direction);
            direction = new Vector2Int(1, -1);
            GetLength(cells, takenPool, position, startType, ref secondDiagonalLength, direction);
            
            return Mathf.Max(horizontalLength, verticalLength, diagonalLength, secondDiagonalLength);
        }

        private static int GetLength(Dictionary<Vector2Int, int> cells, EcsPool<Taken> takenPool, Vector2Int position,
            SignType startType, ref int currentLength, Vector2Int direction)
        {
            Vector2Int currentPosition = position + direction;
            
            while (cells.TryGetValue(currentPosition, out int entity))
            {
                if (takenPool.Has(entity) == false)
                    break;

                SignType type = takenPool.Get(entity).Value;

                if (type != startType)
                    break;

                currentLength++;
                currentPosition += direction;
            }

            return currentLength;
        }
    }
}
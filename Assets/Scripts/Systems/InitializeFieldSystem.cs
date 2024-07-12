using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Components;
using TicTacToe.Configurations;
using UnityEngine;

namespace TicTacToe.Systems
{
    internal class InitializeFieldSystem : IEcsInitSystem
    {
        private EcsCustomInject<GameConfig> _gameConfigInject;
        private EcsCustomInject<GameState> _gameState;
        
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            var gameConfig = _gameConfigInject.Value;
            _world = systems.GetWorld();
            EcsPool<Cell> cellPool = _world.GetPool<Cell>();
            EcsPool<Position> positionPool = _world.GetPool<Position>();
            EcsPool<UpdateCameraEvent> updateCameraEventPool = _world.GetPool<UpdateCameraEvent>();
            
            InitializeField(gameConfig, cellPool, positionPool, updateCameraEventPool);
        }

        private void InitializeField(GameConfig gameConfig, EcsPool<Cell> cellPool, EcsPool<Position> positionPool,
            EcsPool<UpdateCameraEvent> updateCameraEventPool)
        {
            for (int y = 0; y < gameConfig.LevelWidth; y++)
            {
                for (int x = 0; x < gameConfig.LevelHeight; x++)
                {
                    var cellEntity = _world.NewEntity();
                    cellPool.Add(cellEntity);
                    ref Position position = ref positionPool.Add(cellEntity);
                    position.Value = new Vector2Int(x, y);

                    _gameState.Value.Cells[position.Value] = cellEntity;
                }
            }

            updateCameraEventPool.Add(_world.NewEntity());
        }
    }
}

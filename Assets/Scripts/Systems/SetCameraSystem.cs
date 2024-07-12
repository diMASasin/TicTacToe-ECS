using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Configurations;
using UnityEngine;

namespace TicTacToe
{
    public class SetCameraSystem : IEcsInitSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<GameConfig> _gameConfig;
        
        private EcsFilter _filter;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<UpdateCameraEvent>().End();

            SetCamera();
        }

        private void SetCamera()
        {
            if (_filter.GetEntitiesCount() <= 0)
                return;

            Camera camera = _sceneData.Value.Camera;
            int height = _gameConfig.Value.LevelHeight;
            int width = _gameConfig.Value.LevelWidth;
            float positionX = ((width - 1) * (_gameConfig.Value.Offset.x + 1)) / 2;
            float positionY = ((height - 1) * (_gameConfig.Value.Offset.y + 1)) / 2;

            camera.orthographicSize = width / 2f + (width - 1) * _gameConfig.Value.Offset.x / 2;

            _sceneData.Value.CameraTransform.position = new Vector3(positionX, positionY);
        }
    }
}
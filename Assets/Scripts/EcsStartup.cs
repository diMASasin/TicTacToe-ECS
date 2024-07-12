using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Configurations;
using TicTacToe.Systems;
using UnityEngine;

namespace TicTacToe
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SceneData _sceneData;

        private EcsWorld _world;
        private IEcsSystems _systems;
        private GameState _gameState;

        void Start()
        {

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            EcsPool<Taken> takenPool = _world.GetPool<Taken>();
            _gameState = new GameState();
            _sceneData.UI.GameHud.SetTurn(_gameState.CurrentType);
            
            _systems
                .Add(new InitializeFieldSystem())
                .Add(new CreateCellViewSystem())
                .Add(new SetCameraSystem())
                .Add(new ControllSystem())
                .Add(new HandleClickSystem())
                .Add(new CreateTakenViewSystem())
                .Add(new CheckWinSystem())
                .Add(new WinSystem())
                .Add(new DrawSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_gameConfig, _sceneData, _gameState, takenPool)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}
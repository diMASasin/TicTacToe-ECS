using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Components;

namespace TicTacToe
{
    public class HandleClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        
        private EcsFilter _filter;
        private EcsWorld _ecsWorld;
        private EcsPool<Taken> _takenPool;
        private EcsCustomInject<GameState> _gameState;
        private EcsPool<CheckWinEvent> _checkWinPool;

        private GameState GameState => _gameState.Value;

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _filter = _ecsWorld.Filter<Cell>().Inc<Clicked>().Exc<Taken>().End();
            _takenPool = _ecsWorld.GetPool<Taken>();
            _checkWinPool = _ecsWorld.GetPool<CheckWinEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _takenPool.Add(entity).Value = GameState.CurrentType;
                _checkWinPool.Add(entity);
                
                GameState.CurrentType = GameState.CurrentType == SignType.Cross ? SignType.Zero : SignType.Cross;

                _sceneData.Value.UI.GameHud.SetTurn(_gameState.Value.CurrentType);
            }
        }
    }
}
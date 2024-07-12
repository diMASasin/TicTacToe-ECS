using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Components;
using TicTacToe.Configurations;
using TicTacToe.Systems;

namespace TicTacToe
{
    internal class CheckWinSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<GameState> _gameState;
        
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<Taken> _takenPool;
        private EcsPool<Position> _positionPool;
        private EcsPool<Winner> _winnerPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filter = _world.Filter<Position>().Inc<CheckWinEvent>().End();
            
            _positionPool = _world.GetPool<Position>();
            _takenPool = _world.GetPool<Taken>();
            _winnerPool = _world.GetPool<Winner>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref Position position = ref _positionPool.Get(entity);

                int chainLength = _gameState.Value.Cells.GetLongestChain(position.Value, _takenPool);
                
                if (chainLength >= _gameConfig.Value.ChainLength && _winnerPool.Has(entity) == false) 
                    _winnerPool.Add(entity);
            }
        }
    }
}
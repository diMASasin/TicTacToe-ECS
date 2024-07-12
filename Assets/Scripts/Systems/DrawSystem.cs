using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Components;

namespace TicTacToe
{
    internal class DrawSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        
        private EcsWorld _world;
        private EcsFilter _cellsFilter;
        private EcsFilter _winnerFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cellsFilter = _world.Filter<Cell>().Exc<Taken>().End();
            _winnerFilter = _world.Filter<Winner>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_cellsFilter.GetEntitiesCount() == 0 && _winnerFilter.GetEntitiesCount() == 0)
            {
                _sceneData.Value.UI.LoadScreen.Show(true);
            }
        }
    }
}
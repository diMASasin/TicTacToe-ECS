using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Components;
using TicTacToe.Configurations;
using UnityEngine;

namespace TicTacToe
{
    public class CreateCellViewSystem : IEcsInitSystem
    {
        private EcsCustomInject<GameConfig> _gameConfigInject;
        private EcsCustomInject<SceneData> _sceneData;
        private GameConfig GameConfig => _gameConfigInject.Value;

        private EcsFilter _filter;
        private EcsPool<Position> _positions;
        private EcsPool<CellViewRef> _cellViews;
        private EcsWorld _world;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<Cell>().Inc<Position>().Exc<CellViewRef>().End();
            _positions = _world.GetPool<Position>();
            _cellViews = _world.GetPool<CellViewRef>();
            
            CreateCells();
        }

        private void CreateCells()
        {
            foreach (int entity in _filter)
            {
                ref Position position = ref _positions.Get(entity);

                var cellView = Object.Instantiate(GameConfig.CellView, _sceneData.Value.CellsParent);
                cellView.Entity = entity;
                cellView.transform.position = new Vector3(position.Value.x + GameConfig.Offset.x * position.Value.x,
                    position.Value.y + GameConfig.Offset.y * position.Value.y);

                _cellViews.Add(entity).Value = cellView;
            }
        }
    }
}
using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TicTacToe.Configurations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TicTacToe
{
    public class CreateTakenViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameConfig> _gameConfig; 
        
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<CellViewRef> _cellViewRefPool;
        private EcsPool<Taken> _takenPool;
        private EcsPool<TakenRef> _takenRefPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<Taken>().Inc<CellViewRef>().Exc<TakenRef>().End();
            _cellViewRefPool = _world.GetPool<CellViewRef>();
            _takenPool = _world.GetPool<Taken>();
            _takenRefPool = _world.GetPool<TakenRef>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                Vector3 cellPosition = _cellViewRefPool.Get(entity).Value.transform.position;
                SignType cellTaken = _takenPool.Get(entity).Value;

                SignView signView = cellTaken switch
                {
                    SignType.Cross => _gameConfig.Value.CrossView,
                    SignType.Zero => _gameConfig.Value.ZeroView,
                    SignType.None => throw new ArgumentOutOfRangeException(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                SignView instance = Object.Instantiate(signView, cellPosition, Quaternion.identity);
                _takenRefPool.Add(entity).Value = instance;
            }
        }
    }
}
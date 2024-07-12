using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace TicTacToe
{
    public class WinSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<SceneData> _sceneData;

        private EcsFilter _filter;
        private EcsPool<Taken> _takenPool;
        private EcsPool<Winner> _winnerPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<Winner>().Inc<Taken>().End();
            _takenPool = world.GetPool<Taken>();
            _winnerPool = world.GetPool<Winner>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.UI.WinScreen.gameObject.activeInHierarchy == true)
                return;

            foreach (int entity in _filter)
            {
                ref Taken winnerType = ref _takenPool.Get(entity);

                _sceneData.Value.UI.WinScreen.Show(true);
                _sceneData.Value.UI.WinScreen.SetWinner(winnerType.Value);

                _winnerPool.Del(entity);
            }
        }
    }
}
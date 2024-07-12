using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace TicTacToe
{
    public class ControllSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        private EcsPool<Clicked> _clickedPool;

        public void Init(IEcsSystems systems)
        {
            _clickedPool = systems.GetWorld().GetPool<Clicked>();
        }

        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Camera camera = _sceneData.Value.Camera;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out CellView cellView) && _clickedPool.Has(cellView.Entity) == false) 
                        _clickedPool.Add(cellView.Entity);
                }
            }
        }
    }
}
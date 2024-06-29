using Cysharp.Threading.Tasks;
using Entities.Building.Components;
using UnityEngine;

namespace Entities.Building
{
    public class Placement
    {
        private MoveBuilding _moveBuilding;
        private int _layerMask;

        public Placement()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Building");
            Update().Forget();
        }

        private async UniTask Update()
        {
            while (true)
            {
                await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
                
                if (_moveBuilding && _moveBuilding.Placement())
                {
                    _moveBuilding = null;
                    continue;
                }
                
                RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, _layerMask);
                if (hit)
                {
                    _moveBuilding = hit.transform.GetComponent<MoveBuilding>();
                    _moveBuilding.IsMoving();
                }
            }
        }
    }
}

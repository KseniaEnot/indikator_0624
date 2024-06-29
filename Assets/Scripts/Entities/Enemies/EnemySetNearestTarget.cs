using Cysharp.Threading.Tasks;
using SceneContext;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Entities.Enemies
{
    public class EnemySetNearestTarget : MonoBehaviour
    {
        [SerializeField] private EnemyMove _enemyMove;
        [SerializeField] private EnemyAttack _enemyAttack;
        private BuildingsSpawner _buildingsSpawner;

        [Inject]
        private void Construct(BuildingsSpawner buildingsSpawner)
        {
            _buildingsSpawner = buildingsSpawner;
            _buildingsSpawner.OnChangingBuildingsPositions += GetNearestTarget;
            GetNearestTarget();
        }

        private void OnDestroy()
        {
            _buildingsSpawner.OnChangingBuildingsPositions -= GetNearestTarget;
        }

        private void GetNearestTarget() => 
            GetNearestTargetAsync().Forget();

        private async UniTask GetNearestTargetAsync()
        {
            await UniTask.WaitWhile(() => !_enemyMove.Agent.isOnNavMesh);
            
            float shortestPath = float.MaxValue;
            float currentPath;
            Transform nearestPosition = transform;
            
            foreach (var (_, building) in _buildingsSpawner.Buildings)
            {
                currentPath = GetPathLength(_enemyMove.Agent, building.position);
                if (shortestPath > currentPath)
                {
                    shortestPath = currentPath;
                    nearestPosition = building;
                }
            }
            
            _enemyMove.SetTarget(nearestPosition);
            _enemyAttack.SetTarget(nearestPosition);
        } 
        
        private float GetPathLength(NavMeshAgent agent, Vector3 toPos)
        {
            NavMeshPath path = new NavMeshPath();
       
            if (agent != null && agent.CalculatePath(toPos, path) == false )
                return float.MaxValue;
       
            return GetPathLength(path);
        }
       
        private float GetPathLength(NavMeshPath path)
        {
            float length = 0.0f;
       
            if (path.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
            {
                for(int i = 1; i < path.corners.Length; ++i)
                    length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
       
            return length;
        }
    }
}
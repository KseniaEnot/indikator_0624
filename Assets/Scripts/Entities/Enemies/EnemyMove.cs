using SceneContext;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Entities.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        
        private BuildingsSpawner _buildingsSpawner;

        [Inject]
        private void Construct(BuildingsSpawner buildingsSpawner) => 
            _buildingsSpawner = buildingsSpawner;

        private void Awake()
        {
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            
            Agent.SetDestination(_buildingsSpawner.MainBuilding.transform.position);
        }

        public void SetTarget(Transform target)
        {
            Agent.SetDestination(target.position);
        }

    }
}
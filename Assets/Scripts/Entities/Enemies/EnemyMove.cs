using SceneContext;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Entities.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }

        private void Awake()
        {
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
        }

        public void SetTarget(Transform target)
        {
            Agent.SetDestination(target.position);
        }
    }
}
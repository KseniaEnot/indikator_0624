using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Enemies
{
    public class Agr : MonoBehaviour
    {
        [field: SerializeField] private float _agraRadius;
        [SerializeField] private EnemyMove _enemyMove;

        private Transform _target;
        
        private int _buildingsLayerMask;

        private void Awake()
        {
            _buildingsLayerMask = LayerMask.NameToLayer("Building");
        }

        void Update()
        {
            if(!_target)
                return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _agraRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.layer == _buildingsLayerMask)
                {
                    _target = collider.transform;
                    _enemyMove.SetTarget(_target);
                }
            }
            //_enemyMove.ResetTarget();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_target && col.gameObject.layer != _buildingsLayerMask)
                return;
            
            _target = col.transform;
            _enemyMove.SetTarget(_target);
        }
    }
}
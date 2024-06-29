using System;
using Cysharp.Threading.Tasks;
using Entities.HP;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _timeAttack;
        [SerializeField] private float _damage;
        [SerializeField] Animator animator;

        private Transform _target;
        private IHealth _targetHealth;
        private bool _isDestroy;

        public void SetTarget(Transform target)
        {
            _target = target;
            _targetHealth = target.GetComponent<IHealth>();
            Attack().Forget();
        }

        private void OnDestroy()
        {
            _isDestroy = true;
        }

        private async UniTask Attack()
        {
            animator.SetBool("IsDamage", true);
            while (_target)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeAttack));
                
                if (!_isDestroy && (transform.position - _target.position).magnitude < _radius) 
                    _targetHealth.DoDamage(_damage);
            }
            
            if (!_isDestroy) 
                animator.SetBool("IsDamage", false);
        }
    }
}
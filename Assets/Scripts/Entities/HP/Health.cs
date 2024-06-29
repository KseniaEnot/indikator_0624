using System;
using UnityEngine;

namespace Entities.HP
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private AEntity _entity;
        
        [SerializeField, Space] private float _value;
        [SerializeField] private float _maxValue;

        public void DoDamage(float damage)
        {
            _value -= damage;
            if(_value <= 0)
                _entity.DestroyEntity();
        }

        public void Regeneration() => 
            _value = _maxValue;
    }
}

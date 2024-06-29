using System;
using UnityEngine;

namespace Entities.HP
{
    public class HealthActive : MonoBehaviour, IHealth
    {
        public event Action OnUnactivated;
        [SerializeField] private float _value;
        [SerializeField] private float _maxValue;

        public void DoDamage(float damage)
        {
            _value -= damage;
            if(_value <= 0)
            {
                gameObject.SetActive(false);
                OnUnactivated?.Invoke();
            }
        }

        public void Active()
        {
            gameObject.SetActive(true);
            _value = _maxValue;
        }
    }
}
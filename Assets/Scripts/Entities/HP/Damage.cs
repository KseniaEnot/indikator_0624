using System;
using UnityEngine;

namespace Entities.HP
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private float _value;

        private void OnCollisionEnter2D(Collision2D col)
        {
            IHealth health = col.gameObject.GetComponent<IHealth>();
            health?.DoDamage(_value);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IHealth health = col.gameObject.GetComponent<IHealth>();
            health?.DoDamage(_value);
        }
    }
}

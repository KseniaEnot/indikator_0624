using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class DestroyOnTouch : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col) => 
            Destroy(gameObject);

        private void OnTriggerEnter2D(Collider2D col) => 
            Destroy(gameObject);
    }
}
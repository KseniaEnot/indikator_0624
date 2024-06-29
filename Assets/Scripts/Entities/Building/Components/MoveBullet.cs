using System.Collections;
using UnityEngine;

namespace Entities.Buildings.Components
{
    public class MoveBullet : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private void Awake() => 
            StartCoroutine(DestroyByTimer());

        private void OnCollisionEnter(Collision other) => 
            Destroy(gameObject);

        private void Update() => 
            transform.position += transform.up * _speed * Time.deltaTime;

        private IEnumerator DestroyByTimer()
        {
            yield return new WaitForSeconds(8);
            Destroy(gameObject);
        }
    }
}
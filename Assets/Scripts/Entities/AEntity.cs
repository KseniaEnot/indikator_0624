using UnityEngine;

namespace Entities
{
    public abstract class AEntity : MonoBehaviour
    {
        public string ID { get; protected set; }
        public abstract void DestroyEntity();
    }
}
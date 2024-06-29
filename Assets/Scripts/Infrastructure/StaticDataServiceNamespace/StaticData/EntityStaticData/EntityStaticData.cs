using UnityEngine;

namespace ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData
{
    [CreateAssetMenu(menuName = "StaticData/Entity", fileName = "EntityStaticData")]
    public class EntityStaticData : ScriptableObject
    {
        [field: SerializeField] public EntityType EntityType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
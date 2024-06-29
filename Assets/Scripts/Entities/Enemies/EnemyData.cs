using System;
using UnityEngine;

namespace Entities.Enemies.Enemies
{
    [Serializable]
    public class EnemyData
    {
        [field: SerializeField] public int PointsForDestruction { get; private set; }
    }
}
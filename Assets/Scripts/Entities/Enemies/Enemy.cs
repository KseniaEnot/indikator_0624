using System;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Enemies.Enemies
{
    public class Enemy : AEntity
    {
        [field: SerializeField] public EnemyData EnemyData { get; private set; }

        private EnemiesSpawner _enemiesSpawner;

        [Inject]
        private void Construct(EnemiesSpawner enemiesSpawner) => 
            _enemiesSpawner = enemiesSpawner;

        private void Awake() => 
            ID = Guid.NewGuid().ToString();

        private void Start() => 
            _enemiesSpawner.EnemyWasCreated(ID, this);

        public override void DestroyEntity()
        {
            _enemiesSpawner.EnemyWasDestroyed(ID);
            Destroy(gameObject);
        }
    }
}
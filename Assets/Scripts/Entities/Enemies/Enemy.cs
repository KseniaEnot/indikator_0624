using System;
using Entities.HP;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Enemies.Enemies
{
    public class Enemy : AEntity
    {
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] private Health _health;
        [field: SerializeField] private EnemyAttack _enemyAttack;

        private EnemiesSpawner _enemiesSpawner;
        private WaveController _waveController;

        [Inject]
        private void Construct(EnemiesSpawner enemiesSpawner, WaveController waveController)
        {
            _waveController = waveController;
            _enemiesSpawner = enemiesSpawner;
        }

        private void Awake()
        {
            ID = Guid.NewGuid().ToString();
            _health.AddToMax(_waveController.WavesCount * 1.3f);
        }

        private void Start() => 
            _enemiesSpawner.EnemyWasCreated(ID, this);

        public override void DestroyEntity()
        {
            _enemyAttack?.DestroyEnemy();
            _enemiesSpawner.EnemyWasDestroyed(ID);
            Destroy(gameObject);
        }
        public void ForcedDestroyEntity()
        {
            _enemyAttack?.DestroyEnemy();
            Destroy(gameObject);
        }
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities.Enemies.Enemies;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class EnemiesSpawner
    {
        public Action<EnemyData> OnEnemyWasDestroyed;
        
        public Dictionary<string, Enemy> Enemies { get; }
        
        private float _lastWaveStartTime;

        private DiContainer _diContainer;
        private StaticDataService _staticDataService;
        private WaveController _waveController;
        private TimeController _timeController;
        private GameModelStaticData _gameModelStaticData;

        private EnemiesSpawner(DiContainer diContainer, StaticDataService staticDataService, WaveController waveController, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _diContainer = diContainer;
            _waveController = waveController;
            _timeController = timeController;

            Enemies = new Dictionary<string, Enemy>();

            _waveController.OnWaveStart += WaveStart;
            
            _gameModelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }

        public void ClearAllEnemies()
        {
            foreach (KeyValuePair<string,Enemy> enemy in Enemies) 
                GameObject.Destroy(enemy.Value.gameObject);
            Enemies.Clear();
        }
        
        public void EnemyWasDestroyed(string id)
        {
            if(!Enemies.TryGetValue(id, out var enemy))
                return;
            EnemyData enemyData = enemy.EnemyData;
            Enemies.Remove(id);
            OnEnemyWasDestroyed?.Invoke(enemyData);
        }

        public void EnemyWasCreated(string id, Enemy enemy) => 
            Enemies.Add(id, enemy);

        private void WaveStart(int waveNumber)
        {
            if(waveNumber % _gameModelStaticData.WaveWithBoss == 0)
                _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Boss).Prefab, _gameModelStaticData.EnemiesSpawnPoint, Quaternion.identity);
            else
                Spawn(_gameModelStaticData.EnemiesCount, _gameModelStaticData.EnemiesSpawnPoint, _gameModelStaticData.TimeBetweenSpawn).Forget();
        }

        private async UniTask Spawn(int enemiesCount, Vector3 spawnPoints, float timeBetweenSpawn = 0)
        {
            while (enemiesCount > 0)
            {
                enemiesCount--;
                _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Enemy).Prefab, spawnPoints, Quaternion.identity);
                
                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= timeBetweenSpawn);
            }
        }
    }
}
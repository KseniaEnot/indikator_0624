using System;
using Cysharp.Threading.Tasks;
using Entities.HP;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Building
{
    public class MainBuilding : AEntity
    {
        [SerializeField] private GameObject _model;
        
        [SerializeField, Space] private Health _health;
        [SerializeField] private float _timeReactivated;

        private WaveController _waveController;
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private EnemiesSpawner _enemiesSpawner;

        private GameModelStaticData _gameModelStaticData;
        private float _lastWaveStartTime;


        [Inject]
        public void Construct(StaticDataService staticDataService,
            TimeController timeController,
            WaveController waveController,
            EnemiesSpawner enemiesSpawner)
        {
            _timeController = timeController;
            _waveController = waveController;
            _staticDataService = staticDataService;
            _enemiesSpawner = enemiesSpawner;
            
            _gameModelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }

        private void Awake() => 
            ID = Guid.NewGuid().ToString();

        public override void DestroyEntity()
        { 
            _model.SetActive(false);
            
            _enemiesSpawner.ClearAllEnemies();
            int _wavesFromSave = _waveController.WavesCount % _gameModelStaticData.WaveWithBoss;
            if(_wavesFromSave > 0)
                _waveController.DropOn(_wavesFromSave);
            else
                _waveController.DropOn(_gameModelStaticData.WaveWithBoss);
            Active().Forget();
        }

        private async UniTask Active()
        {
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _timeReactivated);

            _health.Regeneration();
            _model.SetActive(true);
        }
    }
}
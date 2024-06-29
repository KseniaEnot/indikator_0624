using System;
using Cysharp.Threading.Tasks;
using Entities.HP;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Building
{
    public class MainBuilding : AEntity
    {
        [SerializeField] private GameObject _model;
        
        [SerializeField, Space] private MainBuildingHealth _health;
        [SerializeField] private float _timeReactivated;
        [SerializeField] private int _defoltScore = 7;

        private WaveController _waveController;
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private EnemiesSpawner _enemiesSpawner;

        private GameModelStaticData _gameModelStaticData;
        private float _lastWaveStartTime;
        private Counter _counter;
        private GameWindowsManager _gameWindowsManager;

        [Inject]
        public void Construct(StaticDataService staticDataService,
            TimeController timeController,
            WaveController waveController,
            EnemiesSpawner enemiesSpawner, 
            Counter counter,
            GameWindowsManager gameWindowsManager)
        {
            _gameWindowsManager = gameWindowsManager;
            _timeController = timeController;
            _waveController = waveController;
            _staticDataService = staticDataService;
            _enemiesSpawner = enemiesSpawner;
            _counter = counter;

            _gameModelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }

        private void Awake() => 
            ID = Guid.NewGuid().ToString();

        public override void DestroyEntity()
        { 
            _model.SetActive(false);
            
            _enemiesSpawner.ClearAllEnemies();
            _waveController.DropOn();
            _counter.DropOn(_defoltScore);
            _gameWindowsManager.Open(EWindow.Menu);
            Active().Forget();
        }

        private async UniTask Active()
        {
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _timeReactivated);

            _model.SetActive(true);
        }
    }
}
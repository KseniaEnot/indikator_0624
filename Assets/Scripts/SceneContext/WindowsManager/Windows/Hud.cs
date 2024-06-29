using System;
using Cysharp.Threading.Tasks;
using Entities;
using Entities.Building;
using Infrastructure.DataServiceNamespace;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Hud : Window
    {
        [SerializeField] private TMP_Text _waveNumber;
        [SerializeField, Space] private TMP_Text _counterView;
        [SerializeField, Space] private TMP_Text _buildingCost;

        [SerializeField, Space] private Button _openMenu;
        [SerializeField] private Button _addBuilding;
        
        private StaticDataService _staticDataService;
        private BuildingsSpawner _buildingsSpawner;
        private DataService _dataService;
        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private Counter _counter;
        
        private EntityStaticData _buildingStaticData;

        [Inject]
        private void Construct(
            StaticDataService staticDataService,
            BuildingsSpawner buildingsSpawner,
            DataService dataService,
            GameWindowsManager gameWindowsManager,
            WaveController waveController,
            Counter counter)
        {
            _staticDataService = staticDataService;
            _buildingsSpawner = buildingsSpawner;
            _dataService = dataService;
            _gameWindowsManager = gameWindowsManager;
            _waveController = waveController;
            _counter = counter;

            _buildingStaticData = _staticDataService.GetEntityStaticData(EntityType.Building);

            _counter.OnScoreChanged += ScoreChanged;
            _waveController.OnWaveStart += WaveStart;
            _waveController.OnWavesDropped += WaveStart;
        }

        private void Awake()
        {
            _openMenu.onClick.AddListener(() => _gameWindowsManager.Open(EWindow.Menu));
            _addBuilding.onClick.AddListener(() => AddBuildingAsync().Forget());
        }

        private async UniTask AddBuildingAsync()
        {
            if (!AddBuilding())
            {
                _addBuilding.image.color = Color.red;
                
                await UniTask.Delay(TimeSpan.FromSeconds(.5));
                
                _addBuilding.image.color = Color.white;
            }
        }

        private bool AddBuilding()
        {
            if (_counter.Score - _dataService.BuildingCost >= 0 
                && _buildingsSpawner.CreateBuildToPlacement(_buildingStaticData.Prefab))
            {
                _counter.AddPoints(-_dataService.BuildingCost);
                return true;
            }
            return false;
        }

        private void OnDestroy() => 
            _counter.OnScoreChanged -= ScoreChanged;

        public override void Open() => 
            gameObject.SetActive(true);

        public override void Close()=> 
            gameObject.SetActive(false);

        private void WaveStart(int waveNumber)
        {
            _waveNumber.text = waveNumber.ToString();
            _buildingCost.text = _dataService.BuildingCost.ToString();
        }

        private void ScoreChanged(int score) => 
            _counterView.text = score.ToString();
    }
}

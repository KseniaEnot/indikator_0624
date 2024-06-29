using System;
using Cysharp.Threading.Tasks;
using Entities;
using Infrastructure.DataServiceNamespace;
using ProjectContext;
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

        private DataService _dataService;
        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private Counter _counter;
        private BuildingCreationArea _buildingCreationArea;

        [Inject]
        private void Construct(DataService dataService,
            GameWindowsManager gameWindowsManager,
            WaveController waveController,
            Counter counter,
            BuildingCreationArea buildingCreationArea)
        {
            _dataService = dataService;
            _gameWindowsManager = gameWindowsManager;
            _waveController = waveController;
            _counter = counter;
            _buildingCreationArea = buildingCreationArea;

            _counter.OnScoreChanged += ScoreChanged;
            _waveController.OnWaveStart += WaveStart;
            _waveController.OnWavesDropped += WaveStart;
        }

        private void Awake()
        {
            _openMenu.onClick.AddListener(() => _gameWindowsManager.Open(EWindow.Menu));
            _addBuilding.onClick.AddListener(() => AddBuilding().Forget());
        }

        private async UniTask AddBuilding()
        {
            if (!_buildingCreationArea.AddBuilding())
            {
                _addBuilding.image.color = Color.red;
                
                await UniTask.Delay(TimeSpan.FromSeconds(.5));
                
                _addBuilding.image.color = Color.white;
            }
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

using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrappingScene : MonoBehaviour
    {
        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private BuildingsSpawner _buildingsSpawner;
        private Counter _counter;

        [Inject]
        private void Construct(GameWindowsManager gameWindowsManager, WaveController waveController, BuildingsSpawner buildingsSpawner, Counter counter)
        {
            _counter = counter;
            _gameWindowsManager = gameWindowsManager;
            _waveController = waveController;
            _buildingsSpawner = buildingsSpawner;
        }

        private void Awake()
        {
            _buildingsSpawner.CreateMainBuilding(GameModelName.GameModelTest);
            
            _waveController.Awake();
            
            _gameWindowsManager.Awake();
            _gameWindowsManager.OpenHud();
            _gameWindowsManager.Open(EWindow.Menu);
            
            _counter.AddPoints(3);
        }
    }
}
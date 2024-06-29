using ProjectContext.WindowsManager;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrappingScene : MonoBehaviour
    {
        private GameWindowsManager _gameWindowsManager;

        [Inject]
        private void Construct(GameWindowsManager gameWindowsManager)
        {
            _gameWindowsManager = gameWindowsManager;
        }

        private void Awake()
        {
            _gameWindowsManager.Awake();
            _gameWindowsManager.OpenHud();
            _gameWindowsManager.Open(EWindow.Menu);
        }
    }
}
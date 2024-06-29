using ProjectContext;
using ProjectContext.WindowsManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Menu : Window
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _exit;
    
        private TimeController _timeController;
        private GameWindowsManager _gameWindowsManager;

        [Inject]
        private void Construct(TimeController timeController, GameWindowsManager gameWindowsManager)
        {
            _timeController = timeController;
            _gameWindowsManager = gameWindowsManager;
        }
    
        private void Awake()
        {
            _play.onClick.AddListener(_gameWindowsManager.Close);
            _exit.onClick.AddListener(Application.Quit);
        }
        
        public override void Open()
        {
            _timeController.Pause();
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            _timeController.Play();
            gameObject.SetActive(false);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UI;
using UI.Hud;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ProjectContext.WindowsManager
{
    public class GameWindowsManager
    {
        public Hud Hud { get; private set; }
        
        private string WindowsPath = "Prefabs/UI/Windows";

        private Dictionary<EWindow, GameObject> _windowPrefabs;
        private Dictionary<EWindow, Window> _windows;

        private Window _openWindow;
        private IInstantiator _instantiator;

        public GameWindowsManager(IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _windowPrefabs = Resources.LoadAll<Window>(WindowsPath)
                .ToDictionary(window => window.EWindow, window => window.gameObject);
        }

        public void Awake()
        {
            _windows = new Dictionary<EWindow, Window>();
            foreach (KeyValuePair<EWindow,GameObject> windowPrefab in _windowPrefabs)
            {
                Window window = _instantiator.InstantiatePrefab(windowPrefab.Value).GetComponent<Window>();
                _windows.Add(windowPrefab.Key, window);
                window.Close();
            }

            Hud = (Hud) _windows[EWindow.Hud];
        }

        public void OpenHud()
        { 
            _windows[EWindow.Hud].Open();
        }

        public void Open(EWindow eWindow)
        {
            _openWindow?.Close(); 
            _openWindow = _windows[eWindow];
            _openWindow.Open();
        }

        public void Close()
        {
            _openWindow?.Close();
            _openWindow = null;
        }

        private Window CreateWindow(EWindow eWindow)
        { 
            GameObject windowPrefab = _windowPrefabs[eWindow];
            return _instantiator.InstantiatePrefab(windowPrefab)
                .GetComponent<Window>();
        }
    }
}
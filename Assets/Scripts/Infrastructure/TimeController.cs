using System;
using UnityEngine;
using Zenject;

namespace ProjectContext
{
    public class TimeController
    {
        public Action OnPause;
        public Action OnPlay;
        public Action<float> OnSetTimeSpeed;
        
        public DateTime FirstLaunchDate;
        public bool IsPause { get; private set; }

        public float CurrentTime => Time.time;
        public float TimeSpeed
        {
            get => Time.timeScale;
            private set
            {
                Time.timeScale = value;
                OnSetTimeSpeed?.Invoke(value);
            }
        }

        private const string FirstLaunchDateKey = "FirstLaunchDate";
        
        private float _timeToPause;

        public TimeController()
        {
            SetTimeSpeed(Time.timeScale);
            LoadFirstLaunchDate();
        }

        public void Pause()
        {
            TimeSpeed = 0;
            
            IsPause = true;
            OnPause?.Invoke();
        }
        
        public void Play()
        {
            TimeSpeed = _timeToPause;
            
            IsPause = false;
            OnPlay?.Invoke();
        }
        
        public void SetTimeSpeed(float timeSpeed)
        {
            TimeSpeed = timeSpeed;
            _timeToPause = timeSpeed;
        }

        private void LoadFirstLaunchDate()
        {
            FirstLaunchDate = DateTime.Now;
            if (PlayerPrefs.HasKey(FirstLaunchDateKey))
            {
                FirstLaunchDate = DateTime.Parse(
                    PlayerPrefs.GetString(FirstLaunchDateKey));
                
                Debug.Log($"Load FirstLaunchDate: {FirstLaunchDate}");
            }
            else
            {
                string firstLaunchDateString = FirstLaunchDate.ToString("G");
                PlayerPrefs.SetString(FirstLaunchDateKey, firstLaunchDateString);
                
                Debug.Log($"Save FirstLaunchDate: {FirstLaunchDate}");
            }
        }
    }
}
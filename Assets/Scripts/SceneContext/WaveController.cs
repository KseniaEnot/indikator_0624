using System;
using Cysharp.Threading.Tasks;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;

namespace SceneContext
{
    public class WaveController
    {
        public Action<int> OnWaveStart;
        public Action<int> OnWavesDropped ;
        
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private float _lastWaveStartTime;
        
        private GameModelStaticData _levelStaticData;
        public int WavesCount { get; private set; }

        private WaveController(StaticDataService staticDataService, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _timeController = timeController;

            Awake();
        }

        public void Awake()
        {
            _levelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
            WaveStart().Forget();
        }

        public void DropOn()
        {
            WavesCount = 0;
            OnWavesDropped?.Invoke(WavesCount);
        }

        public void DropOn(int howMuchToDrop)
        {
            WavesCount -= howMuchToDrop;
            OnWavesDropped?.Invoke(WavesCount);
        }

        private async UniTask WaveStart()
        {
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeStartWaves);
            
            while (true)
            {
                WavesCount++;
                OnWaveStart?.Invoke(WavesCount);

                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeBetweenWaves);
            }
        }
    }
}
using System;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.HP
{
    public class MainBuildingHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private AEntity _entity;
        
        private Counter _counter;

        [Inject]
        public void Construct(Counter counter)
        {
            _counter = counter;
            _counter.OnScoreChanged += ScoreChange;
        }

        public void DoDamage(float damage)
        {
            if(_counter.Score - damage >= 0)
                _counter.AddPoints(-(int) damage);
            else
                _entity.DestroyEntity();
        }

        private void ScoreChange(int damage)
        {
            if(_counter.Score <= 0)
                _entity.DestroyEntity();
        }
    }
}
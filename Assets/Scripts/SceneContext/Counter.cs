using System;
using Entities.Enemies.Enemies;

namespace SceneContext
{
    public class Counter
    {
        public Action<int> OnScoreChanged;
        public int Score { get; private set; }
        
        public Counter(EnemiesSpawner enemiesSpawner)
        {
            enemiesSpawner.OnEnemyWasDestroyed += EnemyWasDestroyed;
        }

        public void AddPoints(int point)
        {
            Score += point;
            OnScoreChanged?.Invoke(Score);
        }

        private void EnemyWasDestroyed(EnemyData enemyData) => 
            AddPoints(enemyData.PointsForDestruction);
    }
}
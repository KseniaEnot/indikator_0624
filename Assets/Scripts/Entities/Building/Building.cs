using System;
using Entities.Building.Components;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Building
{
    public class Building : AEntity
    {
        public event Action<Building> OnDestroyed;

        [field: SerializeField] public int Level { get; private set; } = 1;

        [field: SerializeField] public MoveBuilding MoveBuilding { get; private set; }

        private BuildingsSpawner _buildingsSpawner;

        [Inject]
        public virtual void Construct(BuildingsSpawner buildingsSpawner) => 
            _buildingsSpawner = buildingsSpawner;

        private void Awake()
        {
            ID = Guid.NewGuid().ToString();
            _buildingsSpawner.BuildingWasCreated(ID, transform);
        }

        public override void DestroyEntity()
        {
            OnDestroyed?.Invoke(this);
            _buildingsSpawner.BuildingWasDestroyed(ID);
            Destroy(gameObject);
        }

        public void Stack(Building building)
        {
            Level += building.Level;
            Destroy(building.gameObject);
        }
    }
}
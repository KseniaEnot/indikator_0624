using System.Collections.Generic;
using Entities.Building;
using Infrastructure.DataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class BuildingCreationArea : MonoBehaviour
    {
        [SerializeField] private List<Cell> _placementCells;

        private StaticDataService _staticDataService;
        private DataService _dataService;
        private Counter _counter;
        private BuildingsSpawner _buildingsSpawner;
        
        private EntityStaticData _buildingStaticData;

        [Inject]
        private void Construct(StaticDataService staticDataService,
            DataService dataService,
            Counter counter,
            BuildingsSpawner buildingsSpawner)
        {
            _counter = counter;
            _dataService = dataService;
            _staticDataService = staticDataService;
            _buildingsSpawner = buildingsSpawner;

            _buildingStaticData = _staticDataService.GetEntityStaticData(EntityType.Building);
        }

        public bool AddBuilding()
        {
            foreach (Cell cell in _placementCells)
            {
                if (!cell.Building && _counter.Score - _dataService.BuildingCost >= 0)
                {
                    _counter.AddPoints(-_dataService.BuildingCost);
                    Building.Building building = _buildingsSpawner.CreateBuild(_buildingStaticData.Prefab);
                    building.MoveBuilding.Placement(cell);
                    return true;
                }
            }
            return false;
        }
    }
}
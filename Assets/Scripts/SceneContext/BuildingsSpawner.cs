using System.Collections.Generic;
using Entities;
using Entities.Building;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class BuildingsSpawner
    {
        private IInstantiator _instantiator;
        private Placement _placement;
        private StaticDataService _staticDataService;

        public MainBuilding MainBuilding { get; private set; }

        public Dictionary<string, Transform> Buildings { get; }

        private BuildingsSpawner(StaticDataService staticDataService, IInstantiator instantiator, Placement placement)
        {
            _staticDataService = staticDataService;
            _instantiator = instantiator;
            _placement = placement;

            Buildings = new Dictionary<string, Transform>();
        }

        public void CreateMainBuilding(GameModelName gameModelTest)
        {
            GameModelStaticData gameModelStaticData = _staticDataService.GetGameModelStaticData(gameModelTest);
            EntityStaticData mainBuildingStaticData = _staticDataService.GetEntityStaticData(EntityType.MainBuilding);
            MainBuilding = _instantiator.InstantiatePrefab(mainBuildingStaticData.Prefab,
                    gameModelStaticData.PositionMainBuilding, Quaternion.identity)
                .GetComponent<MainBuilding>();
        }

        public Building CreateBuild(GameObject buildingPrefab)
        {
            GameObject buildingGO = _instantiator.InstantiatePrefab(buildingPrefab, Helper.WorldMousePosition(), Quaternion.identity);
            Building building = buildingGO.GetComponent<Building>();
            return building;
        }

        public bool CreateBuildToPlacement(GameObject buildingPrefab)
        {
            if(!_placement.NotEmpty)
            {
                Building building = CreateBuild(buildingPrefab);
                _placement.SetBuilding(building);
                return true;
            }
            return false;
        }

        public void BuildingWasDestroyed(string id) => 
            Buildings.Remove(id);

        public void BuildingWasCreated(string id, Transform building) => 
            Buildings.Add(id, building);
    }
}
using System.Collections.Generic;
using Entities;
using Entities.Building;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class BuildingsSpawner
    {
        private IInstantiator _instantiator;
        private Placement _placement;

        public Dictionary<string, Transform> Buildings { get; }

        private BuildingsSpawner(IInstantiator instantiator, Placement placement)
        {
            _instantiator = instantiator;
            _placement = placement;

            Buildings = new Dictionary<string, Transform>();
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
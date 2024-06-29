using Entities.Building.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Building
{
    public class Cell : MonoBehaviour
    {
        [field: SerializeField] public Building Building { get; private set; }

        private void Awake()
        {
            if(!Building)
                return;
            Building.MoveBuilding.Placement(this);
            Building.OnDestroyed += RemoveBuilding;
        }

        public void PlaceBuilding(MoveBuilding moveBuilding)
        {
            if (Building == moveBuilding.Building) 
                return;
            if (Building)
                Building.Stack(moveBuilding.Building);
            else
                Building = moveBuilding.Building;
        }

        public void RemoveBuilding(Building building)
        {
            if(Building.ID != building.ID)
                return;
            Building.OnDestroyed -= RemoveBuilding;

            Building = null;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;

namespace ProjectContext.StaticDataServiceNamespace
{
    public class StaticDataService
    {
        private Dictionary<GameModelName, GameModelStaticData> _GameModelStaticDataDictionary;
        private Dictionary<EntityType, EntityStaticData> _entityStaticDataDictionary;

        private string GameModelStaticDataPath = "StaticData/GameModel";
        private string EntityStaticDataPath = "StaticData/Entities";

        public StaticDataService()
        {
            _GameModelStaticDataDictionary = Resources.LoadAll<GameModelStaticData>(GameModelStaticDataPath)
                .ToDictionary(staticData => staticData.GameModelName, staticData => staticData);
            
            _entityStaticDataDictionary = Resources.LoadAll<EntityStaticData>(EntityStaticDataPath)
                .ToDictionary(staticData => staticData.EntityType, staticData => staticData);
        }

        public GameModelStaticData GetGameModelStaticData(GameModelName gameModelName) => 
            _GameModelStaticDataDictionary[gameModelName];

        public EntityStaticData GetEntityStaticData(EntityType entityType) => 
            _entityStaticDataDictionary[entityType];
    }
}
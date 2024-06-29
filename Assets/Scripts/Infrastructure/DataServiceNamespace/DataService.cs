using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;

namespace Infrastructure.DataServiceNamespace
{
    public class DataService
    {
        public int BuildingCost => _gameModelStaticData.BuildingCost;
        private GameModelStaticData _gameModelStaticData;

        public DataService(StaticDataService staticDataService)
        {
            _gameModelStaticData = staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }
    }
}

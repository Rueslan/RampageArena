using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.StaticData;

namespace Assets.Scripts.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);

        LevelStaticData ForLevel(string sceneKey);

        void Load();
    }
}
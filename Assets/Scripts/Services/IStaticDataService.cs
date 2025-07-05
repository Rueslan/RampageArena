using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);

        LevelStaticData ForLevel(string sceneKey);

        void Load();
    }
}
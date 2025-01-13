using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void LoadMonsters();
    }
}
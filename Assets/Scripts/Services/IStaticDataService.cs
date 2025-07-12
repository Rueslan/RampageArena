using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.StaticData;
using Assets.Scripts.StaticData.Windows;
using Assets.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);

        LevelStaticData ForLevel(string sceneKey);

        WindowConfig ForWindow(WindowId shop);
    }
}
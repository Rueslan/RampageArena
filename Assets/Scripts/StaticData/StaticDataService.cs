using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services;
using Assets.Scripts.StaticData.Windows;
using Assets.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string staticdataMonstersPath = "StaticData/Monsters";
        private const string staticdataLevelsPath = "StaticData/Levels";
        private const string staticdataWindowsPath = "StaticData/UI/WindowStaticData";

        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(staticdataMonstersPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(staticdataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(staticdataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowsConfig) ? windowsConfig : null;
        
    }
}

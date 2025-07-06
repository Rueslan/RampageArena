using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using Assets.Scripts.StaticData;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CleanUp();
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateHUD();
        GameObject CreatePlayer(GameObject at);
        //void Register(ISavedProgressReader savedProgress);
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
    }
}
using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        GameObject CreatePlayer(Vector3 at);
        Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
        void CreateTransfer(Vector3 transferDataPosition, string transferDataTransferTo);
        Task WarmUp();
    }
}
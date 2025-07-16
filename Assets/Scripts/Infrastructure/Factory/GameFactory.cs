using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Interfaces;
using Assets.Scripts.StaticData;
using Assets.Scripts.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Logic;
using Assets.Scripts.Logic.EnemySpawners;
using Assets.Scripts.Services;
using Assets.Scripts.UI.Elements;
using Assets.Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;
        private readonly IGameStateMachine _gameStateMachine;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject PlayerGameObject { get; set; }

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService progressService, IWindowService windowService, IGameStateMachine gameStateMachine)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
            _windowService = windowService;
            _gameStateMachine = gameStateMachine;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.Loot);
            await _assets.Load<GameObject>(AssetAddress.Spawner);
        }

        public GameObject CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetAddress.HudPrefabPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.PlayerProgress.WorldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return hud;
        }

        public GameObject CreatePlayer(Vector3 at)=>
            PlayerGameObject = InstantiateRegistered(AssetAddress.PlayerPrefabPath, at);

        public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);
            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHealth = monsterData.Hp;
            health.MaxHealth = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(PlayerGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(PlayerGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.AttackCooldown = monsterData.AttackCooldown;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(PlayerGameObject.transform);

            return monster;
        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Loot);

            var lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.PlayerProgress.WorldData);

            return lootPiece;
        }

        public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);

            SpawnPoint spawnPoint = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();

            spawnPoint.Construct(this);
            spawnPoint.Id = spawnerId;
            spawnPoint.MonsterTypeId = monsterTypeId;

            spawnPoint.LoadProgress(_progressService.PlayerProgress);
        }

        public void CreateTransfer(Vector3 transferDataPosition, string transferDataTransferTo)
        {
            LevelTransferTrigger transferTrigger = InstantiateRegistered(AssetAddress.Transfer, transferDataPosition)
                .GetComponent<LevelTransferTrigger>();

            transferTrigger.Construct(_gameStateMachine);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.CleanUp();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}

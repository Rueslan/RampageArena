using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        public bool Slain => _slain;

        [SerializeField] private bool _slain;
        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;


        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();

        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnDeath += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnDeath -= Slay;

            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}

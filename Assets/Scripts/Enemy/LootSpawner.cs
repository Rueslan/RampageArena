using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;

        private IGameFactory _factory;
        private IRandomService _random;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory gameFactory, IRandomService random)
        {
            _factory = gameFactory;
            _random = random;
        }

        private void Start()
        {
            EnemyDeath.OnDeath += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            loot.Initialize(lootItem);

            EnemyDeath.OnDeath -= SpawnLoot;
        }

        private Loot GenerateLoot()
        {
            return new Loot
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}

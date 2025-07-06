using System;
using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class LootData: ISavedProgress
    {
        public int Collected;
        public Action Changed;
        public Dictionary<string, Vector3Data> LootPositions = new();
        private IGameFactory _factory;
        private IRandomService _random;
        private int _lootMin;
        private int _lootMax;

        public LootData(IGameFactory gameFactory, IRandomService random)
        {
            _factory = gameFactory;
            _random = random;
        }

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LootPositions = progress.WorldData.LootData.LootPositions;

            foreach (var kvp in LootPositions)
            {
                string id = kvp.Key;
                Vector3Data position = kvp.Value;

                LootPiece loot = _factory.CreateLoot();
                loot.transform.position = position.AsUnityVector();
                loot.Initialize(GenerateLoot());
            }

            Collected = progress.WorldData.LootData.Collected;
            Changed?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LootData.Collected = Collected;
            progress.WorldData.LootData.LootPositions = LootPositions;
        }

        private Loot GenerateLoot()
        {
            return new Loot
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }
    }
}
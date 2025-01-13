using Assets.Scripts.Data;
using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LootData: ISavedProgress
{
    public int Collected;
    public Action Changed;
    public Dictionary<string, Vector3Data> LootPositions = new();
    private IGameFactory _factory;

    public LootData(IGameFactory gameFactory)
    {
        _factory = gameFactory;
    }

    public void Collect(Loot loot)
    {
        Collected += loot.Value;
        Changed?.Invoke();
    }

    public void LoadProgress(PlayerProgress progress)
    {
        LootPositions = progress.WorldData.LootData.LootPositions;
        LootPiece loot = _factory.CreateLoot();
        loot.transform.position = transform.position;

        Loot lootItem = GenerateLoot();
        loot.Initialize(lootItem);

        Collected = progress.WorldData.LootData.Collected;
        Changed?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.LootData.Collected = Collected;
        progress.WorldData.LootData.LootPositions = LootPositions;
    }
}
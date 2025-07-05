using System;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;

[Serializable]
public class WorldData
{    
    public PositionOnLevel PositionOnLevel;
    public LootData LootData;

    public WorldData(string initialLevel)
    {
        PositionOnLevel = new PositionOnLevel(initialLevel);
        LootData = new LootData(AllServices.Container.Single<IGameFactory>(), AllServices.Container.Single<IRandomService>());
    }
}

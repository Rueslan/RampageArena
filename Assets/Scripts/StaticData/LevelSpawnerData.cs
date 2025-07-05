using System;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [Serializable]
    public class LevelSpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> EnemySpawners;

        public List<LevelTransferData> Transfers;

        public Vector3 InitialPlayerPosition;
    }
}
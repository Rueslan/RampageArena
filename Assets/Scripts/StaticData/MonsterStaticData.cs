using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName ="MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        public int MinLoot;
        public int MaxLoot;

        [Range(1f,100f)]
        public float Hp = 100;

        [Range(1f, 40f)]
        public float Damage = 20f;

        [Range(0.5f, 3f)]
        public float EffectiveDistance = 3f;

        [Range(0.5f, 3f)]
        public float Cleavage = 0.5f;

        [Range(0.5f, 10f)]
        public float MoveSpeed = 3f;

        [Range(0.1f, 10f)]
        public float AttackCooldown = 3f;

        public AssetReferenceGameObject PrefabReference;
    }
}

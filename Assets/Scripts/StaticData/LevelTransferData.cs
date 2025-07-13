using System;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [Serializable]
    public class LevelTransferData
    {
        public Vector3 Position;
        public string TransferTo;

        public LevelTransferData(string transferTo,  Vector3 position)
        {
            TransferTo = transferTo;
            Position = position;
        }
    }
}
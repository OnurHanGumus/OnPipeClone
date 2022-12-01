using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float ForceX = 10, ForceY = 10;
        public float InitializePosX = -3f, InitializePosY = 1.2f;
        public float SpeedY = 5;
    }
}
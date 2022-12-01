using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public int InitializeCylinderCount = 5;
        public float CylinderMinXZScale = 0.5f, CylinderMaxXZScale = 1f;
        public int CylinderMinYScale = 2, CylinderMaxYScale = 4;
    }
}
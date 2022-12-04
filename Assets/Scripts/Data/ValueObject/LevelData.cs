using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public int InitialCylinderPos = 0;
        public int InitializeCylinderCount = 5;
        public float CylinderMinXZScale = 0.5f, CylinderMaxXZScale = 1f;
        public int CylinderMinYScale = 2, CylinderMaxYScale = 4;
        public int RowWeight = 4;
        public float DistanceBetweenBlocks = 1/3f;

        public int PlayerDrinkScoreIncreaseValue = 2, PlayerDrinkScoreMaksValue = 100;
        public int PlayerScoreIncreaseValue = 1;

        public int ObstacleProbablity = 5;
    }
}
using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class CollectableData
    {
        //public float  = 10, ForceY = 10;
        public float FallForwardForce = 0.5f;
        public Vector3 MaksRotation = Vector3.zero, MinRotation = Vector3.zero;

    }
}
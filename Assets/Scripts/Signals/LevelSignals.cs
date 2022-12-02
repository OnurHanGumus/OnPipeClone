using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public UnityAction onCylinderDisapeared = delegate { };
        public UnityAction onDrinkScoreComplated = delegate { };
        public Func<Transform> onGetTransform = delegate { return null; };
    }
}
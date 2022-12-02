using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayerCollideWithCylinder = delegate { };
        public UnityAction onPlayerInteractedWithCollectable = delegate { };
    }
}
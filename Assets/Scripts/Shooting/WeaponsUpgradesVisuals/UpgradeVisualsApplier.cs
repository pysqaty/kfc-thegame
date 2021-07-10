using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeVisualsApplier : ScriptableObject
{
    public abstract void ApplyVisuals(BasicGun basicGun);
}

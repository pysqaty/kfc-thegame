using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MaterialChangeVisualizer", order = 1)]
public class MaterialChangeVisualizer : UpgradeVisualsApplier
{
    public Material material;
    public override void ApplyVisuals(BasicGun basicGun)
    {
        basicGun.projectileMaterialOverride = material;
    }
}

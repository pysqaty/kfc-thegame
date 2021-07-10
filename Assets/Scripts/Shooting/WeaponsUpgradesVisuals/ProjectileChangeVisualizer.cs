using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProjectileChangeVisualizer", order = 1)]
public class ProjectileChangeVisualizer : UpgradeVisualsApplier
{
    public GameObject projectilePrefab;

    public override void ApplyVisuals(BasicGun basicGun)
    {
        basicGun.projectilePrefab = projectilePrefab;
    }
}

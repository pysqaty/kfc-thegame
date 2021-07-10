using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : ProjectileWeapon
{
    public GameObject Chamber;
    public GameObject Wings;
    public Material projectileMaterialOverride;

    public override void AddUpgrade(WeaponUpgradesData upgradesData)
    {
        base.AddUpgrade(upgradesData);
        upgradesData.visualsApplier.ApplyVisuals(this);

        if(projectileMaterialOverride != null)
        {
            projectilePrefab.GetComponentInChildren<Renderer>().sharedMaterial = projectileMaterialOverride;
        }
    }
}

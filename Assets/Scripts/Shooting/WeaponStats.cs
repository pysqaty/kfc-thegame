using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats
{
    public float reloadTime = 0f;
    public float projectileSpeed = 100f;
    public int ammoCapacity = 0;
    public float accuracyPenalty = 0f;
    public float projectileDelay = 1f;
    public float projectileWidthMultiplier = 1f;
    public float projectileLengthMultiplier = 1f;
    public float projectilesPerFire = 1f;
    public int closeDamage = 10;
    public int midDamage = 10;
    public int farDamage = 10;
    public float closeDamageThreshold = 100f;
    public float midDamageThreshold = 200f;
    public int enemiesToHit = 1;
    public float recoil = 0.1f;
    public float recoilTime = 0.2f;
}

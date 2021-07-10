using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeApplier
{
    public static float ApplyRecoilUpgrade(float recoil, float recoilReduction)
    {
        return recoil * recoilReduction;
    }

    public static int ApplyDamageUpgrade(int damage, float damageUpgrade)
    {
        return Mathf.RoundToInt(damage * damageUpgrade);
    }

    public static int ApplyEnemiesHitUpgrade(int enemiesHit, int enemiesHitUpgrade)
    {
        return enemiesHit + enemiesHitUpgrade;
    }
}

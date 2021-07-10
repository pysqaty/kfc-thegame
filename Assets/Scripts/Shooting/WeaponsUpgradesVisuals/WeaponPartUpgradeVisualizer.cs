using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponPartUpgradeVisualizer", order = 1)]
public class WeaponPartUpgradeVisualizer : UpgradeVisualsApplier
{
    public enum PartType
    {
        Chamber, Wings
    };

    public PartType part;

    public override void ApplyVisuals(BasicGun basicGun)
    {
        switch (part)
        {
            case PartType.Chamber:
                basicGun.Chamber.SetActive(true);
                break;
            case PartType.Wings:
                basicGun.Wings.SetActive(true);
                break;
            default:
                break;
        }
    }
}

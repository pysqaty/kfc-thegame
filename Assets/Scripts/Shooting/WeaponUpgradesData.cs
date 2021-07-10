using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponUpgradesData", order = 1)]
public class WeaponUpgradesData : ScriptableObject
{
    public UpgradeVisualsApplier visualsApplier;
    public string displayname;
    public string description;
    public int cost;

    public float recoilReduction = 1;
    public float closeDamageMultiplier = 1;
    public float midDamageMultiplier = 1;
    public float farDamageMultiplier = 1;
    public int enemiesToHit = 0;
}
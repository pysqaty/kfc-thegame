using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCollectible", menuName = "ScriptableObjects/WeaponCollectible", order = 1)]
public class WeaponCollectible : ScriptableObject
{
    [Header("Model")]
    public GameObject weaponPrefab;
    [Header("Prefab Transform")]
    public Vector3 prefabLocalPos;
    public Vector3 prefabLocalRot;
    public Vector3 prefabLocalScale;
    [Header("Model Transform")]
    public Vector3 modelLocalPos;
    public Vector3 modelLocalRot;
    public Vector3 modelLocalScale;
    [Header("Shooting Point Transform")]
    public Vector3 shootingPointPosLocal;
    public Vector3 shootingPointRotLocal;
    [Header("Right Grip Transform")]
    public Vector3 rightGripPosLocal;
    public Vector3 rightGripRotLocal;
    [Header("Left Grip Transform")]
    public Vector3 leftGripPosLocal;
    public Vector3 leftGripRotLocal;
    [Header("Stats")]
    public WeaponStatsData stats;
}

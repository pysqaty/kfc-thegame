using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] CollectibleWeaponPrefabs;
    private PlayerWeaponsManager weaponsManager;

    private void Start()
    {
        weaponsManager = FindObjectOfType<PlayerWeaponsManager>();
    }

    public void SpawnWeapon()
    {
        List<GameObject> weaponsToSpawn = new List<GameObject>(CollectibleWeaponPrefabs);
        foreach (var w in weaponsManager.CurrentWeapons)
        {
            weaponsToSpawn.RemoveAll(coll => coll.GetComponent<WeaponCollect>().weaponData.stats == w.statsData);
        }

        //Remove currently spawned weapon
        foreach (var coll in GetComponentsInChildren<WeaponCollect>())
        {
            GameObject.Destroy(coll.gameObject);
        }

        if (weaponsToSpawn.Count == 0)
        {
            return;
        }

        int idx = Random.Range(0, weaponsToSpawn.Count);
        GameObject weapon = weaponsToSpawn[idx];

        GameObject go = GameObject.Instantiate(weapon, Vector3.zero, Quaternion.identity, GetRandomSpawnPoint());
        go.transform.localPosition = Vector3.zero;
    }

    private Transform GetRandomSpawnPoint()
    {
        int childNo = Random.Range(0, transform.childCount);
        return transform.GetChild(childNo);
    }
}

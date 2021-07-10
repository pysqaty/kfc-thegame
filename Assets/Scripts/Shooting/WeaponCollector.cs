using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollector : MonoBehaviour
{
    public PlayerWeaponsManager weaponsContainer;

    private void OnTriggerEnter(Collider other)
    {
        var weaponCollect = other.gameObject.GetComponent<WeaponCollect>();
        if(weaponCollect != null)
        {
            var data = weaponCollect.weaponData;
            var addedWeapon = Instantiate(data.weaponPrefab, weaponsContainer.transform);
            addedWeapon.transform.localPosition = data.prefabLocalPos;
            addedWeapon.transform.localRotation = Quaternion.Euler(data.prefabLocalRot);
            addedWeapon.transform.localScale = data.prefabLocalScale;

            foreach (Transform tr in addedWeapon.transform)
            {
                if (tr.tag == "GunModel")
                {
                    tr.localPosition = data.modelLocalPos;
                    tr.localRotation = Quaternion.Euler(data.modelLocalRot);
                    tr.localScale = data.modelLocalScale;
                }
                else if(tr.tag == "ShootPoint")
                {
                    tr.localPosition = data.shootingPointPosLocal;
                    tr.localRotation = Quaternion.Euler(data.shootingPointRotLocal);
                }
                else if(tr.tag == "LeftGrip")
                {
                    tr.localPosition = data.leftGripPosLocal;
                    tr.localRotation = Quaternion.Euler(data.leftGripRotLocal);
                }
                else if(tr.tag == "RightGrip")
                {
                    tr.localPosition = data.rightGripPosLocal;
                    tr.localRotation = Quaternion.Euler(data.rightGripRotLocal);
                }
            }

            var newWeapon = addedWeapon.GetComponent<ProjectileWeapon>();
            newWeapon.statsData = data.stats;
            newWeapon.stats = data.stats.Stats;
            newWeapon.camera = Camera.main.transform;
            weaponsContainer.AddWeapon(newWeapon);

            Destroy(other.gameObject);
        }
    }
}

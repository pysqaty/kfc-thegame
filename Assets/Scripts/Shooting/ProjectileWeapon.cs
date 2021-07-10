using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWeapon : MonoBehaviour, IShootable
{
    public Transform leftGrip;
    public Transform rightGrip;

    public Transform shootingPoint;
    public GameObject projectilePrefab;
    public Transform camera;

    public WeaponStatsData statsData;
    public GameObject shootEffect;

    public WeaponStats stats;
	
    public SoundManager.SfxType firingSfx;

    public string displayName;
    
    private const float RAY_RANGE = 1000f;
    private const float RAYCAST_ROTATION_IGNORE_DISTANCE = 7f;

    private float projectileFiredTime;
    private float reloadStartTime;

    public bool Reloading { get; private set; }
    public int AmmoLeft { get; private set; }

    private List<WeaponUpgradesData> upgrades;

    private void Awake()
    {
        upgrades = new List<WeaponUpgradesData>();
    }

    private void Start()
    {
        stats = statsData.Stats;
        AmmoLeft = stats.ammoCapacity;
        Reloading = false;
    }

    public virtual void AddUpgrade(WeaponUpgradesData upgradesData)
    {
        upgrades.Add(upgradesData);
        stats.accuracyPenalty = UpgradeApplier.ApplyRecoilUpgrade(stats.accuracyPenalty, upgradesData.recoilReduction);
        stats.closeDamage = UpgradeApplier.ApplyDamageUpgrade(stats.closeDamage, upgradesData.closeDamageMultiplier);
        stats.midDamage = UpgradeApplier.ApplyDamageUpgrade(stats.midDamage, upgradesData.midDamageMultiplier);
        stats.farDamage = UpgradeApplier.ApplyDamageUpgrade(stats.farDamage, upgradesData.farDamageMultiplier);
        stats.enemiesToHit = UpgradeApplier.ApplyEnemiesHitUpgrade(stats.enemiesToHit, upgradesData.enemiesToHit);
    }
    
    public void Reload()
    {
        if (!Reloading && AmmoLeft > 0 && AmmoLeft != stats.ammoCapacity)
        {
            Reloading = true;
            reloadStartTime = Time.time;
        }
    }

    private bool CanShoot()
    {
        if (AmmoLeft != 0 && !Reloading)
        {
            if (Time.time - projectileFiredTime > stats.projectileDelay)
            {
                return true;
            }

            return false;
        }
        else
        {
            if (Reloading && Time.time - reloadStartTime > stats.reloadTime)
            {
                Reloading = false;
                AmmoLeft = stats.ammoCapacity;
                return true;
            }

            if (!Reloading && AmmoLeft == 0)
            {
                Reloading = true;
                reloadStartTime = Time.time;
            }

            return false;
        }
    }

    private void FireProjectile()
    {
        for (int i = 0; i < stats.projectilesPerFire; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = shootingPoint.position;
            newProjectile.transform.localScale = new Vector3(
                newProjectile.transform.localScale.x * stats.projectileWidthMultiplier,
                newProjectile.transform.localScale.y * stats.projectileWidthMultiplier,
                newProjectile.transform.localScale.z * stats.projectileLengthMultiplier);
            newProjectile.transform.rotation = shootingPoint.rotation;
            newProjectile.transform.rotation = Quaternion.Euler(
                shootingPoint.rotation.eulerAngles.x + Random.Range(-stats.accuracyPenalty, stats.accuracyPenalty),
                shootingPoint.rotation.eulerAngles.y + Random.Range(-stats.accuracyPenalty, stats.accuracyPenalty),
                shootingPoint.rotation.eulerAngles.z);
            LaserProjectileScript projectileScript = newProjectile.GetComponent<LaserProjectileScript>();
            if (projectileScript != null)
            {
                projectileScript.startPosition = shootingPoint.position;
                projectileScript.speed = stats.projectileSpeed;
                projectileScript.closeDamage = stats.closeDamage;
                projectileScript.midDamage = stats.midDamage;
                projectileScript.farDamage = stats.farDamage;
                projectileScript.closeThreshold = stats.closeDamageThreshold;
                projectileScript.midThreshold = stats.midDamageThreshold;
                projectileScript.enemiesToHit = stats.enemiesToHit;
            }
        }
        projectileFiredTime = Time.time;
        if (stats.ammoCapacity != -1)
        {
            AmmoLeft--;
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            if (shootEffect != null)
            {
                var systems = shootEffect.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem system in systems)
                {
                    system.Play();
                }
            }
            SoundManager.Instance.PlaySfx(firingSfx);
			FireProjectile();
            WeaponKick();
        }
    }

    private void Update()
    {
        CanShoot();
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, RAY_RANGE))
        {
            if(hit.distance >= RAYCAST_ROTATION_IGNORE_DISTANCE)
            {
                transform.LookAt(hit.point);
            }
        }
        else
        {
            transform.LookAt(camera.position + camera.forward * 1000f);
        }
    }


    private bool readyToRecoil = true;

    public void WeaponKick()
    {
        if(!readyToRecoil)
        {
            return;
        }
        var recoilPos = transform.localPosition - new Vector3(0.0f, 0.0f, stats.recoil);
        StartCoroutine(Recoil(transform, recoilPos, stats.recoilTime));
    }

    IEnumerator Recoil(Transform trn, Vector3 recoilPos, float time)
    {
        readyToRecoil = false;
        Vector3 start = trn.localPosition;
        float curTime = 0;
        while (curTime < time / 2)
        {
            curTime += Time.deltaTime;
            trn.localPosition = Vector3.Slerp(start, recoilPos, curTime / time * 2);
            yield return null;
        }
        curTime = 0;
        while (curTime < time / 2)
        {
            curTime += Time.deltaTime;
            trn.localPosition = Vector3.Slerp(recoilPos, start, curTime / time * 2);
            yield return null;
        }
        trn.localPosition = start;
        readyToRecoil = true;
    }
}
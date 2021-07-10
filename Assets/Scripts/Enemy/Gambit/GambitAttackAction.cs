using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambitAttackAction : EnemyAction
{
    public Transform shootingPoint;
    public GameObject projectilePrefab;

    public int damage = 10;
    public float projectileSpeed = 30f;
    public float projectileRange = 100f;
    public int projectilesPerFire = 4;
    public float delayBetweenShots = 1f;
	
    public SoundManager.SfxType shotSfx = SoundManager.SfxType.SzulerChickShot;
	
    protected override void Start()
    {
        base.Start();
    }

    protected override void Setup()
    {
        Vector3 shootDir = playerInfo.transform.position - transform.position;
        shootDir.y += playerInfo.TargetHeight;
        shootDir.Normalize();
        Vector3 enemyDir = new Vector3(shootDir.x, 0, shootDir.z);
        gameObject.transform.forward = enemyDir;

        StartCoroutine(Attack(shootDir));
    }

    protected override void Perform()
    {

    }

    private IEnumerator Attack(Vector3 shootDir)
    {
        animationController.SetToggleAnimation(EnemyAnimationType.ATTACK, true);
        for (int i = 0; i < projectilesPerFire; i++)
        {
            SoundManager.Instance.PlaySfxShot(shotSfx);
			GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = shootingPoint.position;
            newProjectile.transform.forward = shootDir;
            EnemyProjectile projectileScript = newProjectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Owner = enemyInfo;
                projectileScript.speed = projectileSpeed;
                projectileScript.damage = damage;
            }
            if(i != projectilesPerFire - 1)
            {
                yield return new WaitForSeconds(delayBetweenShots);
            }
		}
        Finish();
        animationController.SetToggleAnimation(EnemyAnimationType.ATTACK, false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RegularAttackAction : EnemyAction
{
    public Transform ShootingPoint;
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public float PreFireDelay;
    public float ProjectileDelay;
    public int ProjectilesCount;
    public int Damage;

    NavMeshAgent nav;

    bool isTurning = false;
    float rotationSpeed = 2;
    float totalTime = 0;
    float angSpeed = 0;
    Quaternion startRotation;

    protected override void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
    }

    protected override void Setup()
    {
        angSpeed = nav.angularSpeed;
        nav.angularSpeed = 0; // stop him turning like an idiot
        startRotation = transform.rotation;
        isTurning = true;
        totalTime = 0;
    }

    protected override void Perform()
    {
        if (isTurning)
        {
            totalTime += Time.deltaTime;
            Vector3 playerDir = playerInfo.transform.position - transform.position;
            playerDir.y = 0;
            Quaternion rot = Quaternion.LookRotation(playerDir);
            float t = totalTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, rot, t);
            if (t >= 1)
            {
                Vector3 shootDir = playerInfo.transform.position - ShootingPoint.position;
                shootDir.y += playerInfo.TargetHeight;
                shootDir.Normalize();

                isTurning = false;
                StartCoroutine(Attack(shootDir));
            }
        }
    }

    private IEnumerator Attack(Vector3 dir)
    {
        animationController.TriggerAnimation(EnemyAnimationType.ATTACK);
        yield return new WaitForSeconds(PreFireDelay);

        for (int i = 0; i < ProjectilesCount; i++)
        {
            GameObject projectile = Instantiate(ProjectilePrefab);
            projectile.transform.position = ShootingPoint.position;
            projectile.transform.forward = dir;
            EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Owner = enemyInfo;
                projectileScript.speed = ProjectileSpeed;
                projectileScript.damage = Damage;
            }

            if (i < ProjectilesCount - 1)
            {
                yield return new WaitForSeconds(ProjectileDelay);
            }
        }

        animationController.TriggerAnimation(EnemyAnimationType.STOP_ATTACK);
        nav.angularSpeed = angSpeed;
        Finish();
    }
}

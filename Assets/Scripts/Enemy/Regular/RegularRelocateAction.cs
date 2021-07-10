using UnityEngine;
using UnityEngine.AI;

public class RegularRelocateAction : EnemyAction
{
    public float Angle = 90f;
    public float MinPlayerDistance;
    public int MaxRandomCount = 10;

    const float targetDist = 10.0f;
    NavMeshAgent nav;
    Vector3 target;

    protected override void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
    }

    protected override void Setup()
    {
        nav.enabled = true;
        nav.ResetPath();
        nav.stoppingDistance = targetDist;
        nav.speed = enemyInfo.Speed;

        // choose a point 
        Vector3 fromEnemy = transform.position - playerInfo.transform.position;
        if (fromEnemy.sqrMagnitude > 0)
        {
            fromEnemy.Normalize();
        }
        else
        {
            fromEnemy = Vector3.forward;
        }

        for (int i = 0;; i++)
        {
            if(i == MaxRandomCount)
            {
                Debug.Log("Could not relocate");
                Finish();
                return;
            }

            target = Assets.Scripts.Utility.Math.GetPosition(playerInfo.transform.position, fromEnemy,
                                                         Random.Range(-Angle, Angle), Random.Range(MinPlayerDistance, Range));

            NavMeshPath path = new NavMeshPath();
            nav.CalculatePath(target, path);

            if (path.status == NavMeshPathStatus.PathComplete && nav.SetDestination(target))
            {
                animationController.TriggerAnimation(EnemyAnimationType.WALK);
                animationController.SetToggleAnimation(EnemyAnimationType.WALK, true);

                break;
            }
        }
    }

    protected override void Perform()
    {
        Vector3 xzPos = transform.position, xzTarget = target;
        xzPos.y = 0;
        xzTarget.y = 0;
        if (Vector3.Distance(xzPos, xzTarget) <= targetDist)
        {
            nav.isStopped = true;
            animationController.SetToggleAnimation(EnemyAnimationType.WALK, false);
            Finish();
        }
    }
}

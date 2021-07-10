using UnityEngine;
using UnityEngine.AI;

public class ChaseAction : EnemyAction
{
    private NavMeshAgent nav;
    private Enemy enemy;

    public Transform LineOfSightTransform { get; set; }

    protected override void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }

    protected override void Setup()
    {
        nav.enabled = true;
        nav.ResetPath();
        //nav.stoppingDistance = Range;
        nav.speed = enemyInfo.Speed;

        animationController.SetToggleAnimation(EnemyAnimationType.WALK, true);
    }

    protected override void Perform()
    {
        if (nav.isOnOffMeshLink)
        {
            GetComponent<EnemyPortalTraveller>().Teleport(transform, transform, nav.currentOffMeshLinkData.endPos, Quaternion.identity);
            nav.CompleteOffMeshLink();
        }

        if (nav.pathPending == false)
        {
            if (nav.SetDestination(playerInfo.transform.position) == false)
            {
                Debug.LogError("Could not calculate path to player");
            }
        }

        if(Vector3.Distance(transform.position, playerInfo.transform.position) < Range && enemy.HasLineOfSight(LineOfSightTransform.position, playerInfo.transform.position, playerInfo.TargetHeight))
        {
            nav.isStopped = true;
            animationController.SetToggleAnimation(EnemyAnimationType.WALK, false);
            Finish();
        }
    }
}
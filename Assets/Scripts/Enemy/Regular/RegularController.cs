using UnityEngine;

public class RegularController : EnemyController
{
    private ChaseAction chaseAction;
    private RegularAttackAction attackAction;
    private RegularRelocateAction relocateAction;
    private Enemy enemy;
    bool doRelocate = true;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        chaseAction = GetComponent<ChaseAction>();
        attackAction = GetComponent<RegularAttackAction>();
        relocateAction = GetComponent<RegularRelocateAction>();

        chaseAction.LineOfSightTransform = attackAction.ShootingPoint;
    }

    public override EnemyAction GetNextAction()
    {
        if (Vector3.Distance(playerInfo.transform.position, transform.position) < attackAction.Range && 
            enemy.HasLineOfSight(attackAction.ShootingPoint.position, playerInfo.transform.position, playerInfo.TargetHeight))
        {
            doRelocate = !doRelocate;
            if (doRelocate)
            {
                Debug.Log("Regular relocating");
                return relocateAction;
            }
            else
            {
                Debug.Log("Regular attacking");
                return attackAction;
            }
        }
        return chaseAction;
    }
}

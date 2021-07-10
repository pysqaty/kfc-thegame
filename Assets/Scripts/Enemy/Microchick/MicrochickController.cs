using UnityEngine;

public class MicrochickController : EnemyController
{
    public Transform LineOfSightPoint;

    private ChaseAction chaseAction;
    private MicrochickAttackAction attackAction;
    private Enemy enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        chaseAction = GetComponent<ChaseAction>();
        attackAction = GetComponent<MicrochickAttackAction>();

        chaseAction.LineOfSightTransform = LineOfSightPoint;
    }

    public override EnemyAction GetNextAction()
    {
        if (Vector3.Distance(playerInfo.transform.position, transform.position) < attackAction.Range &&
            enemy.HasLineOfSight(LineOfSightPoint.position, playerInfo.transform.position, playerInfo.TargetHeight))
        {
            return attackAction;
        }
        return chaseAction;
    }
}

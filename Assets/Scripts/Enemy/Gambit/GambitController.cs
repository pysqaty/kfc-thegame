using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambitController : EnemyController
{
    bool trickNow;
    private Enemy enemy;
    private ChaseAction chaseAction;
    private GambitAttackAction attackAction;
    private GambitTrickAction trickAction;
    protected override void Start()
    {
        base.Start();
        trickNow = true;
        enemy = GetComponent<Enemy>();
        chaseAction = GetComponent<ChaseAction>();
        attackAction = GetComponent<GambitAttackAction>();
        trickAction = GetComponent<GambitTrickAction>();

        chaseAction.LineOfSightTransform = attackAction.shootingPoint;
    }

    public override EnemyAction GetNextAction()
    {
        if (Vector3.Distance(playerInfo.transform.position, transform.position) < attackAction.Range &&
            enemy.HasLineOfSight(attackAction.shootingPoint.position, playerInfo.transform.position, playerInfo.TargetHeight))
        {
            trickNow = !trickNow;
            if (trickNow)
            {
                Debug.Log("Gambit trick");
                return trickAction;
            }
            else
            {
                Debug.Log("Gambit attack");
                return attackAction;
            }
        }
        //Debug.Log("Gambit chase");
        return chaseAction;
    }
}

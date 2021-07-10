using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GambitTrickAction : EnemyAction
{
    public float Angle = 90f;
    public float MinDistance = 4f;
    public float MaxDistance = 6f;
    private TeleportEffect teleportEffect;

    NavMeshAgent nav;


    protected override void Start()
    {
        teleportEffect = GetComponentInChildren<TeleportEffect>();
        nav = GetComponent<NavMeshAgent>();
        base.Start();
    }

    protected override void Setup()
    {
        teleportEffect.Disappear();
        StartCoroutine(RunDelayed(1f, () =>
        {
            Vector3 behindEnemy = -playerInfo.gameObject.transform.forward;
            behindEnemy.Normalize();
            float angle = Random.Range(-Angle, Angle);
            float distance = Random.Range(MinDistance, MaxDistance);
            Vector3 newPos = Assets.Scripts.Utility.Math.GetPosition(playerInfo.transform.position,
               behindEnemy, Random.Range(-Angle, Angle), Random.Range(MinDistance, MaxDistance));

            if(NavMesh.SamplePosition(newPos, out var hit, 1000f, NavMesh.AllAreas) == false)
            {
                teleportEffect.Appear();
                Finish();
                return;
            }

            Vector3 newPosOnMesh = hit.position;
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(newPosOnMesh, playerInfo.transform.position, NavMesh.AllAreas, path);

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                gameObject.transform.position = new Vector3(newPosOnMesh.x, newPosOnMesh.y, newPosOnMesh.z);
                gameObject.transform.forward = playerInfo.gameObject.transform.position - gameObject.transform.position;
            }
            else
            {
                Debug.Log("Teleport destination invalid");
            }

            teleportEffect.Appear();
            Finish();
        }));
        
        
    }

    protected override void Perform()
    {

    }

    private IEnumerator RunDelayed(float time, System.Action delayedAction)
    {
        yield return new WaitForSeconds(time);
        delayedAction.Invoke();
    }
}

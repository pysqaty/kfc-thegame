using System.CodeDom;
using UnityEngine;
using UnityEngine.AI;

public class MicrochickAttackAction : EnemyAction
{
    public float JumpForce;
    public float JumpHeight;

    private Collider collider;
    private Rigidbody chickenbody;
    private AudioSource audioSrc;
    NavMeshAgent nav;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        chickenbody = GetComponent<Rigidbody>();
        chickenbody.isKinematic = true;
    }

    protected override void Setup()
    {
        chickenbody.isKinematic = false;
        nav.enabled = false;
        Vector3 dir = playerInfo.transform.position - transform.position;
        dir.y = JumpHeight;
        chickenbody.AddForce(dir.normalized * JumpForce);
        collider.isTrigger = false;

        animationController.TriggerAnimation(EnemyAnimationType.ATTACK);
    }

    protected override void Perform()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundCollider") || chickenbody.velocity.magnitude < 1e-3)
        {
            animationController.TriggerAnimation(EnemyAnimationType.STOP_ATTACK_IMMEDIATELY);
            chickenbody.isKinematic = true;
            collider.isTrigger = true;
            Finish();
            return;
        }
        else if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySfx(SoundManager.SfxType.ChickDmgDone);
            playerInfo.TakeDamage(5);
            animationController.TriggerAnimation(EnemyAnimationType.STOP_ATTACK);
        }
        else
        {
            animationController.TriggerAnimation(EnemyAnimationType.STOP_ATTACK);
        }
    }
}
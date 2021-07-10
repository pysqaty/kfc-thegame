using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    public float Cooldown;
    public float Range;

    public bool Completed { get; private set; } = true;

    private bool onCooldown = false;
    private float cooldownElapsed;

    protected Enemy enemyInfo;
    protected PlayerController playerInfo;
    protected AnimationController animationController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyInfo = GetComponent<Enemy>();
        playerInfo = FindObjectOfType<PlayerController>();
        animationController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (onCooldown)
        {
            cooldownElapsed += Time.deltaTime;
            if (cooldownElapsed >= Cooldown)
            {
                onCooldown = false;
                Completed = true;
            }
        }
        else if (!Completed)
        {
            Perform();
        }
    }

    // Begin performing action
    public void Begin()
    {
        Completed = false;
        Setup();
    }

    // Operations to execute each time the action begins
    protected abstract void Setup();
    // Action logic
    protected abstract void Perform();

    // Call when all action logic is done
    protected void Finish()
    {
        cooldownElapsed = 0;
        onCooldown = true;
    }
}

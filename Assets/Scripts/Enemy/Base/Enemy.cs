using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{
    private const float MIN_Y = -1e3f;

    public int MaxHealth;
    public int Health;
    public float Speed;
    public int Points;
    public int Currency;

    public UnityEngine.UI.Slider health;
    public GameObject healthBar;
    public ParticleSystem deathEffect;

    [ReadOnly] public EnemyAction Action;

    private EnemyController controller;
    private LayerMask losLayermask;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        health.value = CalculateHealth();
        controller = GetComponent<EnemyController>();

        losLayermask = ~(LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Environment"));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < MIN_Y)
        {
            Health = 0;
        }

        if (healthBar.activeInHierarchy)
        {
            healthBar.transform.LookAt(Camera.main.transform);
        }
        if ( Health < MaxHealth)
        {
            healthBar.SetActive(true);
            health.value = CalculateHealth();
        }
        if (Health <= 0)
        {
            WaveManager.enemiesRemaining--;
            GameManager.score += Points;
            deathEffect.transform.parent = null;
            deathEffect.Play();
            Destroy(deathEffect.gameObject, (deathEffect.main.duration + deathEffect.main.startLifetime.constant) * 2);
            Destroy(gameObject);
        }
        else if (Action == null || Action.Completed)
        {
            Action = controller.GetNextAction();
            Action.Begin();
        }
    }
    private float CalculateHealth()
    {
        return (float)Health / (float)MaxHealth;
    }

    public bool HasLineOfSight(Vector3 rayStart, Vector3 playerPosition, float rayEndHeight)
    {
        playerPosition.y += rayEndHeight;
        Ray ray = new Ray(rayStart, (playerPosition - rayStart).normalized);
        Debug.DrawLine(rayStart, playerPosition);
        if(Physics.Raycast(ray, out var raycastInfo, 100f, losLayermask))
        {
            //Debug.Log($"Hit {raycastInfo.collider.gameObject.name} : {raycastInfo.collider.gameObject.layer}");
            return raycastInfo.collider.gameObject.GetComponent<PlayerController>();
        }
        return false;
    }
}

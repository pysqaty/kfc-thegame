using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected Enemy enemyInfo;
    protected PlayerController playerInfo;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyInfo = GetComponent<Enemy>();
        playerInfo = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public abstract EnemyAction GetNextAction();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float damageReductionTime;

    [ReadOnly] public Enemy Owner;

    private Rigidbody rb;
    private float shotTime;

    private const float lifetime = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shotTime = Time.time;
    }

    private void Update()
    {
        if(Mathf.Abs(Time.time - shotTime) > lifetime)
        {
            Destroy(this.gameObject);
        }
        rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.TakeDamage(damage);
        }
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != Owner)
        {
            Destroy(this.gameObject);
        }
    }
}

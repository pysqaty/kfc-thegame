using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectileScript : MonoBehaviour
{
    public float speed;
    
    public int closeDamage;
    public int midDamage;
    public int farDamage;

    public float closeThreshold;
    public float midThreshold;
    
    public int enemiesToHit;
    
    public Vector3 startPosition;
    private Rigidbody rb;
    private int projectileLayer;

    private float shotTime;
    private const float lifetime = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectileLayer = LayerMask.NameToLayer("Projectile");
        shotTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            float distance = Vector3.Distance(startPosition, transform.position);
            if (distance < closeThreshold)
            {
                enemyScript.Health -= closeDamage;
            }
            else if (distance < midThreshold)
            {
                enemyScript.Health -= midDamage;
            }
            else
            {
                enemyScript.Health -= farDamage;
            }
            enemiesToHit--;
            if (enemiesToHit == 0)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer != projectileLayer)
        {
            //We've hit something that's not the enemy or another projectile
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Mathf.Abs(Time.time - shotTime) > lifetime)
        {
            Destroy(this.gameObject);
        }
        rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }
}

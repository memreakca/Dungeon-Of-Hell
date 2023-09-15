using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public static Fireball main;
    [SerializeField] private float movespeed = 50;
    [SerializeField] private float dmg=1;

    private Rigidbody2D rb;
    private Transform target;
    private bool isDone;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        main = this;
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
        Destroy(gameObject, 3f);   
    }
    private void Update()
    {
        if (!target)
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * movespeed ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDone)
        {
            return;
        }
        

        if (other.CompareTag("Enemy"))
        {
            isDone = true;
            other.gameObject.GetComponent<Enemy>().takeDamage(dmg);
            Destroy(gameObject);            
        }
    }
}

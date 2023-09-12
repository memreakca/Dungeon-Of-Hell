using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitPoint;
    [SerializeField] private float EnemyMs;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float range;
    [SerializeField] private Transform target;
    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range , Mask);

        if(hitColliders.Length > 0)
        {
            foreach(Collider2D col in hitColliders)
            {
                target = col.transform;
            }
            AttackToCh();
        }
    }

    private void AttackToCh()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * EnemyMs * Time.deltaTime);
       
    }

    public void takeDamage(float dmg)
    {
        hitPoint -= dmg;

        if (hitPoint <= 0f)
        {
            Debug.Log("DIED");
            Destroy(gameObject); 
            EnemySpawner.Instance.enemiesAlive--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
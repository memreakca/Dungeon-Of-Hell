using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{

    [SerializeField] public float dmg;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent<Enemy>(out var enemy))
            enemy.takeDamage(dmg);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBombExplosion : MonoBehaviour
{
    [SerializeField] private float explosionDmg;
    
    private void Start()
    {
        Destroy(gameObject,0.6f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(explosionDmg);
            Undamage();
        }
    }
    
    private void Undamage()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
  

}

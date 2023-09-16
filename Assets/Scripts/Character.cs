using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character main;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Transform[] weaponslot;
    //[SerializeField] public LayerMask points;
    //[SerializeField] public Transform[] spwns;

    [Header("Attiributes")]
    [SerializeField] private float ms = 2;
    [SerializeField] private float HitPoints = 15f;
    //[SerializeField] private float spawnrange;
    //[SerializeField] private float antispawnrange;

    private void Awake()
    {
        main = this;
    }
    private void Update()
    {

        //Collider2D[] spawn = Physics2D.OverlapCircleAll(transform.position, spawnrange, points );
        //Collider2D[] antispawn = Physics2D.OverlapCircleAll(transform.position, antispawnrange, points );

        //foreach(Collider2D pnt in spawn )
        //{
        //    Debug.Log(pnt.gameObject.name);
        //    //pnt.enabled = false;
        //    //foreach (Collider2D pnt2 in antispawn)
        //    //{
        //    //    pnt2.enabled = false;
        //    //}
           
        //}
        


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        rb.velocity = movement * ms;
        movement *= ms * Time.deltaTime;
        Vector2 newPosition = rb.position + movement;
    }

    public void takeDamage(float dmg)
    {
        HitPoints -= dmg;

        if (HitPoints <= 0f)
        {
            Debug.Log("You DIED");
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, antispawnrange);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, spawnrange);
    //}
}

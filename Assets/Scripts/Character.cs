using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character main;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Transform[] weaponslot;


    [Header("Attiributes")]
    [SerializeField] private float ms = 2;
    [SerializeField] private float HitPoints = 15f;
    [SerializeField] public float antispawnrange;

    private void Awake()
    {
        main = this;
    }
    private void Update()
    {   

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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, antispawnrange);
    }
}

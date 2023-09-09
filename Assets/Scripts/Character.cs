using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attiributes")]
    [SerializeField] private float ms = 2;

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
}

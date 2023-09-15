using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private float swingSpeed = 50f;
    [SerializeField] private Vector3 offset;
    
    
    public Transform character;  // Reference to the character's transform

    
    private bool canSwing = true;

    private void Start()
    {
        character = Character.main.transform;
        offset = transform.position - character.position;
    }
    private void Update()
    {

        Vector2 desiredPosition = character.position + offset;

        // Rotate the ball around the character.
        transform.RotateAround(character.position, Vector3.forward, swingSpeed * Time.deltaTime);

        // Set the ball's position to the desired position.
        transform.position = desiredPosition;
    }
 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitPoint;

    public void takeDamage(float dmg)
    {
        hitPoint -= dmg;

        if (hitPoint <= 0f)
        {
            Debug.Log("DIED");


        }
    }
}
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class FireMaceAttack : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 50f;
    [SerializeField] private Transform SwingPoint;



    private void Update()
    {
        transform.RotateAround(SwingPoint.position, Vector3.forward, swingSpeed * Time.deltaTime);
    }

}

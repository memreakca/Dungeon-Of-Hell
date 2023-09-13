using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentAttack : MonoBehaviour
{
    public static TridentAttack main;
    [Header("Refs")]
    [SerializeField] public Transform AttackPoint;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform InitialPosition;
    [SerializeField] public Transform normalPosition;
    [SerializeField] public Transform RotationPoint;
    [SerializeField] public Transform HitPoint;

    private Collider2D tridentCollider;

    [Header("Attributes")]
    [SerializeField] private float attackRange;
    [SerializeField] private float dmg;
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] private float RotateRange;

    private float rotationSpeed = 500f;
    private bool isAttacking;
    private bool isHit;
    private Transform target;
    private bool canAttack = true;
    private void Awake()
    {
        main = this;
    }
    private void Update()
    {
            
        tridentCollider = gameObject.GetComponent<Collider2D>();

       
        if (isAttacking)
        {
            tridentCollider.enabled = true;
        }
        else tridentCollider.enabled = false;

        Collider2D[] RotateColliders = Physics2D.OverlapCircleAll(AttackPoint.position, RotateRange, EnemyMask);


        if (RotateColliders.Length > 0)
        {

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D col in RotateColliders)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.transform;
                }
            }
            if (isAttacking == false)
            {
                RotateTowardsTarget();
            }
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, EnemyMask);


        if (hitColliders.Length > 0)
        {

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D col in hitColliders)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.transform;
                }
            }

            if (canAttack)
            {
                Attack();
            }
            else return;
        }
        else if (isAttacking == true)
        {
            CheckIsBack();
        }

    }

      


    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - RotationPoint.position.y, target.position.x - RotationPoint.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        RotationPoint.rotation = Quaternion.RotateTowards(RotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        Vector3 direction = (target.position - RotationPoint.position).normalized;
        RotationPoint.position += (direction * moveSpeed * Time.deltaTime);
        isAttacking = true;
    }

    private void BackToPos()
    {
        RotationPoint.DOLocalMove(normalPosition.localPosition, 1f).SetEase(Ease.OutCubic).OnComplete(ResetAttack);
        isAttacking = false;
    }

    private void ResetAttack()
    {
        isHit = false;
        canAttack = true;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(dmg);
            isHit = true;
            canAttack = false;

            Invoke("BackToPos",0.25f);

        }
    }
    private void CheckIsBack()
    {
        if (!isHit)
        {
            BackToPos();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackPoint.position, RotateRange);
    }

}

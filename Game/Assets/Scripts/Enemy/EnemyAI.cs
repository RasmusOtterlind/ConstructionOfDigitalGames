using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Animator animator;

    public LayerMask groundMask;
    public LayerMask enemyMask;
    public CapsuleCollider capsuleCollider;
    public float desiredDistanceToPlayer = 15f;
    public float runningSpeed = 3f;
   
    public float detectionRange = 100f;
    public float shootDetectionRange = 10f;
    public float shootDelay = 1f;
    private Transform player;

    public Transform eyes;

    public EnemyShoot enemyShoot;
    private bool canShoot;

    private EnemyStats enemyStats;

    public GameObject ragdoll;

    public int goldValue = 10;

    // Start is called before the first frame update
    void Start()
    {
       rigidbody = GetComponent<Rigidbody>();
       capsuleCollider = GetComponent<CapsuleCollider>();
       enemyStats = GetComponent<EnemyStats>();
       canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleExistence();
    }
    
    private void FixedUpdate()
    {
        handleDetection();
        handleMovement();
    }

    private void handleMovement()
    {
        if (player == null)
        {
            return;
        }
        float xDistance = player.transform.position.x - capsuleCollider.transform.position.x;
        
        if (Mathf.Abs(xDistance) > 0.1f)
        {
            rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(player.position.x - capsuleCollider.transform.position.x), 0)));
        }

        if (Mathf.Abs(xDistance) < desiredDistanceToPlayer)
        {
            rigidbody.velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            animator.SetFloat("SpeedX", 0);
            return;
        }

        if (xDistance < 0)
        {
            bool leftClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(-capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore)
                && !Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(-capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, enemyMask, QueryTriggerInteraction.Ignore);
            if (leftClear)
            {
                rigidbody.velocity = new Vector3(-2 * runningSpeed, GetComponent<Rigidbody>().velocity.y, 0);
                animator.SetFloat("SpeedX", -1 * rigidbody.velocity.x);
                return;
            }

        }
        else
        {
            bool rightClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore)
                && !Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, enemyMask, QueryTriggerInteraction.Ignore);
            if (rightClear)
            {
                rigidbody.velocity = new Vector3(2 * runningSpeed, GetComponent<Rigidbody>().velocity.y, 0);
                animator.SetFloat("SpeedX", rigidbody.velocity.x);
                return;
            }
        }
        animator.SetFloat("SpeedX", 0);
    }
    private void handleDetection()
    {
        Collider[] shootColliders = Physics.OverlapSphere(capsuleCollider.transform.position, shootDetectionRange);
        foreach (Collider collider in shootColliders)
        {
            if (collider.tag == "Player" && CanSeeTarget(collider.transform))
            {
                StartCoroutine(ShootWithDelay(collider.transform));
            }
        }

        Collider[] colliders = Physics.OverlapSphere(capsuleCollider.transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                player = collider.transform;
            } 
        }
    }
    
    IEnumerator ShootWithDelay(Transform target)
    {
        if (!canShoot)
        {
            yield break;
        }

        canShoot = false;

        yield return new WaitForSeconds(shootDelay);

        enemyShoot.Shoot(target);

        canShoot = true;
    }

    private bool CanSeeTarget(Transform target)
    {
        Vector3 targetOffsetPosition = target.position;
        targetOffsetPosition.y -= 0.5f;

        Vector3 direction = targetOffsetPosition - transform.position;
        direction.y -= transform.localScale.y - target.localScale.y;

        RaycastHit hit;
        Debug.DrawRay(eyes.position, direction, Color.red);
        if (Physics.Raycast(eyes.position, direction, out hit, shootDetectionRange))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void handleExistence()
    {
        HealthEntity healthEntity = GetComponent<HealthEntity>();
        if (healthEntity != null)
        {
            if(healthEntity.health <= 0)
            {
                if (player.gameObject)
                {
                    GameObject ragdollObject = Instantiate(ragdoll, transform.position, transform.rotation);
                    player.gameObject.GetComponent<PlayerController>().onEnemyKilled(goldValue);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (player == null)
        {
            return;
        }
        Vector3 aimOffset = player.position;
        aimOffset.y += 1;
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, aimOffset);


        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(aimOffset);
    }
}

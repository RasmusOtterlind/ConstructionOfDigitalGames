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
    public float desiredDistanceToPlayer = 5f;
    public float runningSpeed = 3f;
   
    public float detectionRange = 100f;
    private float shootDetectionRange = 10f;
    private Transform player;

    public EnemyShoot enemyShoot;
    private bool canShoot;

    private EnemyStats enemyStats;

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
        
    }
    
    private void FixedUpdate()
    {
        handleDetection();
        handleMovement();
    }

    private void handleMovement()
    {
        float xDistance = player.transform.position.x - capsuleCollider.transform.position.x;
        float yDistance = player.transform.position.y - capsuleCollider.transform.position.y;
        
        if (Mathf.Abs(xDistance) > 0.1f)
        {
            rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(player.position.x - capsuleCollider.transform.position.x), 0)));
        }

        if (Mathf.Abs(xDistance) < desiredDistanceToPlayer && Mathf.Abs(yDistance) < 1f)
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
            if (collider.tag == "Player")
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

        yield return new WaitForSeconds(1f);

        enemyShoot.Shoot(target);

        canShoot = true;
    }

    public void TakeDamage(float damage)
    {
        enemyStats.OnHitByBullet(damage);
        if (enemyStats.getHealth() <= 0f)
        {
            Destroy(gameObject);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Animator animator;

    public LayerMask groundMask;
    public CapsuleCollider capsuleCollider;
    public float desiredDistanceToPlayer = 3f;
    public float runningSpeed = 3f;
   
    public float detectionRange = 100f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
       rigidbody = GetComponent<Rigidbody>();
       capsuleCollider = GetComponent<CapsuleCollider>();
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
        
        rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(player.position.x - capsuleCollider.transform.position.x), 0)));

        if (Mathf.Abs(xDistance) < desiredDistanceToPlayer && Mathf.Abs(yDistance) < 1f)
        {
            rigidbody.velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            animator.SetFloat("SpeedX", 0);
            return;
        }

        if (xDistance < 0)
        {
            bool leftClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(-capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
            if (leftClear)
            {
                rigidbody.velocity = new Vector3(-2 * runningSpeed, GetComponent<Rigidbody>().velocity.x, 0);
                animator.SetFloat("SpeedX", -1 * rigidbody.velocity.x);
            }

        }
        else
        {
            bool rightClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
            if (rightClear)
            {
                rigidbody.velocity = new Vector3(2, GetComponent<Rigidbody>().velocity.x, 0);
                animator.SetFloat("SpeedX", rigidbody.velocity.x);
            }
        }
    }
    private void handleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(capsuleCollider.transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                player = collider.transform;
            } 
        }
    }
}

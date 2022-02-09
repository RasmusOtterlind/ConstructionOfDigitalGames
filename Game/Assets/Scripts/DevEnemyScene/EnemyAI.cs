using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody rigidbody;

    public LayerMask groundMask;
    public CapsuleCollider capsuleCollider;
    public float desiredDistanceToPlayer = 3f;
   
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

        if (Mathf.Abs(xDistance) < desiredDistanceToPlayer) return;

        if (xDistance < 0)
        {
            bool leftClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(-capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
            if (leftClear)
            {
                rigidbody.velocity = new Vector3(-1, GetComponent<Rigidbody>().velocity.x, 0);
            }

        } else
        {
            bool rightClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(capsuleCollider.radius * 2, 0, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
            if (rightClear)
            {
                rigidbody.velocity = new Vector3(1, GetComponent<Rigidbody>().velocity.x, 0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody rigidbody;

    public LayerMask groundMask;
    public CapsuleCollider capsuleCollider;
    public float desiredDistanceToPlayer = 5f;
   
    public float detectionRange = 10f;
    public Transform player;
    public bool isInRange = false;

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

        if(xDistance < 0)
        {
            bool leftClear = Physics.CheckSphere(capsuleCollider.transform.position - new Vector3(0, capsuleCollider.radius, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
            if (leftClear)
            {
                rigidbody.velocity = new Vector3(-1, GetComponent<Rigidbody>().velocity.x, 0);
            }

        } else
        {
            bool rightClear = Physics.CheckSphere(capsuleCollider.transform.position + new Vector3(0, capsuleCollider.radius, 0), capsuleCollider.radius, groundMask, QueryTriggerInteraction.Ignore);
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
                isInRange = true;
            } else
            {
                isInRange = false;
            }
        }
        Debug.Log(isInRange);
    }
}

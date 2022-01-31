using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float input = 0f;
    private bool canJump = false;
    private bool jump = false;

    public Transform groundChecker;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(input * 5, GetComponent<Rigidbody>().velocity.y, 0);
        canJump = Physics.CheckSphere(groundChecker.position, 0.1f, groundMask, QueryTriggerInteraction.Ignore);
        if (jump && canJump)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            jump = false;
        }

    }

}

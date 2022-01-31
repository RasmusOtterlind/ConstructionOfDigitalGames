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

    public Animator animator;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        rigidbody.velocity = new Vector3(input * 5, GetComponent<Rigidbody>().velocity.y, 0);
        //Used for animation transitions
        animator.SetFloat("SpeedX", rigidbody.velocity.x);
        canJump = Physics.CheckSphere(groundChecker.position, 0.1f, groundMask, QueryTriggerInteraction.Ignore);
        if (jump && canJump)
        {
            rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            jump = false;
        }

    }

}

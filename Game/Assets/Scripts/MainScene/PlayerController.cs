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


    //Used for aiming
    public LayerMask aimMask;
    public Transform targetTransform;
    private Camera mainCamera;

    public Animator animator;

    private Rigidbody rigidbody;

    public GameObject bullet;
    public Transform muzzleTransform;


    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        handleAiming();
        input = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //Logic used to determine where the character should be facing
    private int FacingSign
    {
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }

    private void FixedUpdate()
    {
       
        HandleDirection();
        HandleAnimation();  
        HandleJump();  
    }

    private void HandleAnimation()
    {
        rigidbody.velocity = new Vector3(input * 5, GetComponent<Rigidbody>().velocity.y, 0);
        //Used for animation transitions
        animator.SetFloat("SpeedX", FacingSign* rigidbody.velocity.x);
    }

    private void HandleDirection()
    {
        rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));
    }

    private void HandleJump()
    {
        canJump = Physics.CheckSphere(groundChecker.position, 0.1f, groundMask, QueryTriggerInteraction.Ignore);
        if (jump && canJump)
        {
            rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            jump = false;
        }
    }

    private void handleAiming()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimMask))
        {
            targetTransform.position = hit.point;
        }
    }

    private void Shoot()
    {
        Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
        GameObject tempBullet = Instantiate(bullet, muzzleTransform.position, offsetRot);
        tempBullet.GetComponent<BasicBullet>().hitPoint = targetTransform.position;

        Debug.Log(muzzleTransform.localEulerAngles);
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);


        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }



}

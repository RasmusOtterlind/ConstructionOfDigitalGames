
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject gameManager;

    private float input = 0f;
    private bool canJump = false;
    private bool jump = false;

    private float recoil = 0;
    private float shootCooldown = 0;
    private Vector3 hitPoint;
    private Vector3 deltaVector;
    public float fireRate = 4;
    private float recoilIncrease = 0.2f;
    private float maxRecoil = 3f;

    public Transform groundChecker;
    public LayerMask groundMask;
    public LayerMask enemyMask;


    //Used for aiming
    public LayerMask aimMask;
    public Transform targetTransform;
    private Camera mainCamera;

    public Animator animator;

    private Rigidbody rigidbody;

    public GameObject bullet;
    public Transform muzzleTransform;

    //Player Stats
    public float health;
    public float damage;
    public int gold;
    
    // UI components
    public Slider healthSlider;
    public GameObject inventory;
    public TextMeshProUGUI txtGold;
    public TextMeshProUGUI txtHealth;
    public TextMeshProUGUI txtDamage;

    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip hurt3;

    private AudioSource audioSource;

    private void Awake()
    {
        health = PlayerPrefs.GetFloat("health");
        damage = PlayerPrefs.GetFloat("damage");
        gold = PlayerPrefs.GetInt("gold");
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        health = PlayerPrefs.GetFloat("health");
        damage = PlayerPrefs.GetFloat("damage");
        gold = PlayerPrefs.GetInt("gold");

        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jump = true;
        }
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            LowerRecoil();
        }
        
        HandleStats();
        HandleUI();
    }

    private void LowerRecoil()
    {
        shootCooldown += 60 * Time.deltaTime;

        if (recoil > 0 && shootCooldown >= fireRate)
        {
            recoil -= 0.05f;
        }
        
    }

    private void HandleUI()
    {
        if (health < 0)
        {
            health = 0;
        }
        healthSlider.value = health / 100f;
        txtHealth.text = "Health: " + health;
        txtDamage.text = "Damage: " + damage;
    }

    private void HandleStats()
    {
        if (health <= 0)
        {
            health = 0;
            PlayerPrefs.SetInt("gold", gold);
            gameManager.GetComponent<GameManager>().setShowDeadMenu(true);
        }
    }
    
    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        HandlePainSound();
    }

    public void HandlePainSound()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                audioSource.clip = hurt1;
                break;
            case 1:
                audioSource.clip = hurt2;
                break;
            case 2:
                audioSource.clip = hurt3;
                break;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void LateUpdate()
    {
        HandleAiming();
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
        canJump = Physics.CheckSphere(groundChecker.position, 0.1f, groundMask, QueryTriggerInteraction.Ignore)
                || Physics.CheckSphere(groundChecker.position, 0.1f, enemyMask, QueryTriggerInteraction.Ignore);
        if (jump && canJump)
        {
            rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            jump = false;
        }
    }

    private void HandleAiming()
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

        shootCooldown += 60 * Time.deltaTime;
        if (shootCooldown >= fireRate )
        {
            //pretty sure you can normalize the deltaVector in order to scale the recoil but I will have to look at it
            deltaVector = targetTransform.position - muzzleTransform.position;
            
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            //We need to scale the recoil to how close we aim to the muzzle
          
            hitPoint = deltaVector.normalized + muzzleTransform.position;
            hitPoint += new Vector3(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), 0) *0.05f;
            GameObject tempBullet = Instantiate(bullet, muzzleTransform.position, offsetRot);
            tempBullet.GetComponent<BasicBullet>().spawnedByPlayer = true;
            tempBullet.GetComponent<BasicBullet>().damage = damage;
            tempBullet.GetComponent<BasicBullet>().hitPoint = hitPoint;
            shootCooldown = 0;

            if(recoil < maxRecoil)
            {
                recoil += recoilIncrease;
            }

        }
    }

    public void onEnemyKilled()
    {
        gold += 10;
        txtGold.text = "Gold: " + gold;
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);


        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }



}

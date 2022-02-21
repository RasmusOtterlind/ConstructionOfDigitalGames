
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    private float recoilIncrease = 0.01f;
    private float maxRecoil = 1f;

    public Transform groundChecker;
    public LayerMask groundMask;
    public LayerMask enemyMask;

    public bool falldamage = false;

    public Vector3 velocity;

    //Used for aiming
    public LayerMask aimMask;
    public Transform targetTransform;
    private Camera mainCamera;

    public Animator animator;

    private Rigidbody rigidbody;

    [SerializeField] private GameObject flashlight;
    private bool isFlashlightOn = false;

    public GameObject bullet;
    public Transform muzzleTransform;

    private bool canShoot = true;
    
    //Player Stats
    public float damage;
    public int gold;
    
    
    //Inventory
    public GameObject parachute;
    
    // UI components
    public Slider healthSlider;
    public GameObject inventory;
    public TextMeshProUGUI txtGold;
    public TextMeshProUGUI txtHealth;
    public TextMeshProUGUI txtDamage;

    private void Awake()
    {
        damage = PlayerPrefs.GetFloat("damage");
        gold = PlayerPrefs.GetInt("gold");
    }

    // Start is called before the first frame update
    void Start()
    {
        damage = PlayerPrefs.GetFloat("damage");
        gold = PlayerPrefs.GetInt("gold");

        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        flashlight.SetActive(isFlashlightOn);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = gameObject.GetComponent<Rigidbody>().velocity;
        input = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jump = true;
        }
        HandleParachute();
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            canShoot = false;
            Shoot();
        }else if (Input.GetButtonUp("Fire1") && !canShoot)
        {
            canShoot = true;
        }
        else
        {
            LowerRecoil();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
        
        HandleStats();
        HandleUI();
    }
    

    private GameObject openedParachute;
    
    private void HandleParachute()
    {
        if (Input.GetKeyDown("v"))
        {
            if (openedParachute == null)
            {
                openedParachute = Instantiate(parachute, transform.position+Vector3.up, Quaternion.identity);
                openedParachute.transform.parent = gameObject.transform;
                openedParachute.transform.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                Destroy(openedParachute);
                GetComponent<Rigidbody>().useGravity = true;
            }
        }else if (openedParachute != null && GetComponent<Rigidbody>().velocity.y <= 0.5f)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -2, GetComponent<Rigidbody>().velocity.z);
        }
    }

    private void removeParachute()
    {
        Destroy(openedParachute);
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void ToggleFlashlight()
    {
        flashlight.SetActive(!isFlashlightOn);
        isFlashlightOn = !isFlashlightOn;
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
        float health = GetComponent<HealthEntity>().health;
        float maxHealth = GetComponent<HealthEntity>().startHealth;
       
        if (health < 0)
        {
            health = 0;
        }
        healthSlider.value = health / maxHealth;
        txtHealth.text = "Health: " + health;
        txtDamage.text = "Damage: " + damage;
        txtGold.text = "Gold: " + gold;
    }

    private void HandleStats()
    {
        float health = GetComponent<HealthEntity>().health;

        if (health <= 0)
        {
            health = 0;
            PlayerPrefs.SetInt("gold", gold);
            gameManager.GetComponent<GameManager>().setShowDeadMenu(true);
            Destroy(gameObject);
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
        canJump = Physics.CheckSphere(groundChecker.position, 0.2f, groundMask, QueryTriggerInteraction.Ignore)
                || Physics.CheckSphere(groundChecker.position, 0.2f, enemyMask, QueryTriggerInteraction.Ignore);

        if (canJump)
        {
            removeParachute();
            if (falldamage)
            {
                GetComponent<HealthEntity>().takeDamage(10);
                falldamage = false;
            }
        }
        else
        {
            if (velocity.y < -7)
            {
                falldamage = true;
            }
        }
        if (jump && canJump)
        {
            rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            jump = false;
            falldamage = false;
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
        if (shootCooldown >= fireRate)
        {
            //pretty sure you can normalize the deltaVector in order to scale the recoil but I will have to look at it
            deltaVector = targetTransform.position - muzzleTransform.position;
            
            Quaternion offsetRot = new Quaternion(0, -90, 0, 0);
            //We need to scale the recoil to how close we aim to the muzzle
          
            hitPoint = deltaVector.normalized + muzzleTransform.position;
            hitPoint += new Vector3(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), 0) *0.05f;
            GameObject tempBullet = Instantiate(bullet, muzzleTransform.position, offsetRot);
            tempBullet.GetComponent<BasicBullet>().spawnedByPlayer = true;
            //tempBullet.GetComponent<BasicBullet>().damage = damage;
            tempBullet.GetComponent<BasicBullet>().hitPoint = hitPoint;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.pitch = Random.Range(0.90f, 1.10f);
            audioSource.Play();

            shootCooldown = 0;

            if(recoil < maxRecoil)
            {
                recoil += recoilIncrease;
            }

        }
    }

    public void onEnemyKilled(int goldValue)
    {
        gold += goldValue;
        txtGold.text = "Gold: " + gold;
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);


        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }

    public void ResetPlayer()
    {
        PlayerPrefs.SetFloat("health", 100);
        PlayerPrefs.SetFloat("damage", 10);
        PlayerPrefs.SetInt("gold", 0);
        txtGold.text = "Gold: " + gold;
        txtDamage.text = "Damage: " + damage;
        txtHealth.text = "Health: " + GetComponent<HealthEntity>().health;
    }

    public void FetchStatsAfterUpgrade()
    {
        gold = PlayerPrefs.GetInt("gold");
        damage = PlayerPrefs.GetFloat("damage");
        GetComponent<HealthEntity>().health = PlayerPrefs.GetFloat("health");
    }

}

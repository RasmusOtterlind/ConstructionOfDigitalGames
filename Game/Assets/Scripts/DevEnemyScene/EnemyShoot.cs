using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 100;

    public float xLowAim = -0.5f;
    public float xUpperAim = .0f;
    public float yLowAim = -0.5f;
    public float yUpperAim = 2.0f;

    private Transform playerTransform;
    private Transform ownTransform;
    private Transform muzzleTransform;

    public GameObject muzzle;
    public GameObject bulletPrefab;
    private GameObject bullet;

    private Rigidbody bulletRigidBody;

    private bool canShoot;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        ownTransform = GetComponent<Transform>();
        muzzleTransform = muzzle.GetComponent<Transform>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        print(playerTransform.position);

        if (canShoot)
        {
            canShoot = false;

            float xAim = Random.Range(xLowAim, xUpperAim);
            float yAim = Random.Range(yLowAim, yUpperAim);

            bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            bulletRigidBody = bullet.GetComponent<Rigidbody>();
            bulletRigidBody.AddForce(((playerTransform.position + new Vector3(xAim, yAim, 0)) - bullet.GetComponent<Transform>().position).normalized * 15, ForceMode.Impulse);
            Destroy(bullet, 3f);
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}

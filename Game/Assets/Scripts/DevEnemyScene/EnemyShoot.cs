using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 100;

    public Transform playerTransform;
    private Transform ownTransform;
    private Transform muzzleTransform;

    public GameObject muzzle;
    public GameObject bulletPrefab;
    private GameObject bullet;

    private Rigidbody bulletRigidBody;

    private bool canShoot;

    void Start()
    {
        ownTransform = GetComponent<Transform>();
        muzzleTransform = muzzle.GetComponent<Transform>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        muzzleTransform.LookAt(playerTransform);

        if (canShoot)
        {
            canShoot = false;

            bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            bulletRigidBody = bullet.GetComponent<Rigidbody>();
            bulletRigidBody.AddForce((playerTransform.position - bullet.GetComponent<Transform>().position).normalized * 50, ForceMode.Impulse);
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

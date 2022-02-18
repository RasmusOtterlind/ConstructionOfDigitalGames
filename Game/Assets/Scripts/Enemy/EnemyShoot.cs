using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public float xLowAim = -0.5f;
    public float xUpperAim = .0f;
    public float yLowAim = -0.5f;
    public float yUpperAim = 2.0f;

    public float power = 15f;

    private Transform muzzleTransform;

    public GameObject muzzle;
    public GameObject bulletPrefab;
    private GameObject bullet;

    private Rigidbody bulletRigidBody;

    void Start()
    {
        if (muzzle.GetComponent<Transform>() != null)
        {
            muzzleTransform = muzzle.GetComponent<Transform>();
        }
    }
    public void Shoot(Transform target)
    {
        float xAim = Random.Range(xLowAim, xUpperAim);
        float yAim = Random.Range(yLowAim, yUpperAim);

        bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        bulletRigidBody = bullet.GetComponent<Rigidbody>();
        bulletRigidBody.AddForce(((target.position + new Vector3(xAim, yAim, 0)) - bullet.GetComponent<Transform>().position).normalized * power, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }
}

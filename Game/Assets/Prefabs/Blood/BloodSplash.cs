using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    public float timeToLive = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

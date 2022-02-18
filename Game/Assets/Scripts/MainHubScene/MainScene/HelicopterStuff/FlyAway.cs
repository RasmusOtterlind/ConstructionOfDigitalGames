using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{

    private bool eventTriggered;
    private float lifetime = 10f;
    private float rotationtime = 3f;
    private bool goForward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(eventTriggered);
        if (eventTriggered)
        {
            if (transform.position.y < 28)
            {
                transform.Translate(new Vector3(0, 4 * Time.deltaTime, 0));
            }

            if (transform.position.y >= 28 && rotationtime > 0)
            {
                rotationtime -= Time.deltaTime;
                transform.Rotate(new Vector3(7 * Time.deltaTime, 0, 0));
                transform.Translate(new Vector3(0, 0, 3 * Time.deltaTime));
                if (rotationtime < 0)
                {
                    goForward = true;
                }
            }

            if (transform.position.y >= 28 && goForward && rotationtime < 0)
            {
                transform.Translate(new Vector3(0, 0, 11 * Time.deltaTime));
                Destroy(gameObject, lifetime);
            }
        }
    }

    public void ToggleEvent()
    {
        eventTriggered = true;
    }
}

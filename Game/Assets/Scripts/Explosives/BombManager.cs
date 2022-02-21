using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public Transform player;
    public GameObject airPlaneBomb;
    public float initialDelay = 15;
    public float intenisty = 0.05f;

    private float startTime = 0;
    private float compareTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        compareTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + initialDelay && player != null)
        {
            if(Time.time > compareTime + 1)
            {
                float random = Random.Range(0f, 1f);
                if (random < intenisty)
                {
                    Debug.Log(random);
                    Instantiate(airPlaneBomb, player.position + new Vector3(0, 75, 0), Quaternion.Euler(90f, 0f, 0f));
                }
                compareTime = Time.time;
            }
        }  
    }
}

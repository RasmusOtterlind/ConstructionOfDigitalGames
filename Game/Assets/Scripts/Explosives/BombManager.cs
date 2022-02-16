using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public Transform player;
    public GameObject airPlaneBomb;

    private float lastBomb = 0;
    private float randomTimeAddition;
    // Start is called before the first frame update
    void Start()
    {
        randomTimeAddition = Random.Range(1, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastBomb + randomTimeAddition) {
            Instantiate(airPlaneBomb, player.position + new Vector3(0, 100, 0), Quaternion.Euler(90f, 0f, 0f));
            randomTimeAddition = Random.Range(3, 15f);
            lastBomb = Time.time;   
        }
    }
}

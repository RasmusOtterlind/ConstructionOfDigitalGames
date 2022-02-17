using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] enemyPrefabs;
    public float spawnChance = 0.9f;
    

    void Start()
    {

        
        if(spawnChance > Random.Range(0f, 1f) && transform.parent.position != null)
        {
            GameObject spawnedPrefab = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.parent.position + transform.position, Quaternion.identity);
            spawnedPrefab.transform.position = new Vector3(spawnedPrefab.transform.position.x, spawnedPrefab.transform.position.y, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
   
    public GameObject[] itemPrefabs;
    public float spawnChance = 0.5f;
    
    void Start()
    {

        
        if(spawnChance > Random.Range(0f, 1f) && transform.parent.position != null)
        {
            GameObject spawnedPrefab = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], transform.parent.position + transform.position, Quaternion.identity);
            spawnedPrefab.transform.position = new Vector3(spawnedPrefab.transform.position.x, spawnedPrefab.transform.position.y, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

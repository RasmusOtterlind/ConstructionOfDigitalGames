using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
   
    public GameObject[] itemPrefabs;
    public float spawnChance = 1f;
    
    void Start()
    {
        if(spawnChance > Random.Range(0f, 1f) && transform.parent.position != null)
        {
            print("Spawn item");
            GameObject spawnedPrefab = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], transform.position, Quaternion.identity);
            spawnedPrefab.transform.Rotate(new Vector3(0, 200, 0));
            spawnedPrefab.transform.position = new Vector3(spawnedPrefab.transform.position.x, spawnedPrefab.transform.position.y, 0f);
            print("Spawn item2");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

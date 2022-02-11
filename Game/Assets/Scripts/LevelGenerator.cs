using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    
    public GameObject[] chunkPrefabs = new GameObject[5];
    private float nextPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 spawnPosition = new Vector3(5 + nextPosition, Random.Range(0, 0), 0f);
            GameObject ground = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], spawnPosition, Quaternion.identity);
            nextPosition += ground.GetComponent<ChunkSize>().size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

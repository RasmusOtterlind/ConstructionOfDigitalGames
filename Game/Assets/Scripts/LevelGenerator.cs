using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    
    public GameObject[] chunkPrefabs;
    public GameObject bossChunk;
    public GameObject endChunk;
    private float nextPosition = 5f;

    private float prevChunksize = 0;

    public int levelSize = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelSize; i++)
        {
            Debug.Log(nextPosition);
            Vector3 spawnPosition = new Vector3(nextPosition, Random.Range(0, 0), -0.6f);
            GameObject ground = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], spawnPosition, Quaternion.identity);
            
            nextPosition += ground.GetComponent<ChunkSize>().size;
        }
        SpawnFinalChunks();
    }

    private void SpawnFinalChunks()
    {
        Vector3 spawnBossPosition = new Vector3(13.66f + nextPosition, 0, -1.44f);
        GameObject boss = Instantiate(bossChunk, spawnBossPosition, Quaternion.Euler(0f, 180f, 0f));
        nextPosition += boss.GetComponent<ChunkSize>().size;

        Vector3 spawnEndPosition = new Vector3(nextPosition, 0, -0.6f);
        GameObject end = Instantiate(endChunk, spawnEndPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

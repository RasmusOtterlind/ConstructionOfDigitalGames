using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{


    public Transform navTransform;

    private NavMeshAgent meshAgent;

    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshAgent.destination = navTransform.position;
        
    }
}

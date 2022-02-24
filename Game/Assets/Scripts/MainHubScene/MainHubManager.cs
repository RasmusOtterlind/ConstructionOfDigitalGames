using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHubManager : MonoBehaviour
{

    [SerializeField] private GameObject bombManager;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        bombManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

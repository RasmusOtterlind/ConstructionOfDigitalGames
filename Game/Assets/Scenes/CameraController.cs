using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform focus;
    public Vector3 offset;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        Vector3 tempVector = focus.position;
        tempVector += offset;
        transform.position = tempVector;
    }
}

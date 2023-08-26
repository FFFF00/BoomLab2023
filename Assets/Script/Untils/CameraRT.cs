using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRT : MonoBehaviour
{
    private Camera selfCamera;
    // Start is called before the first frame update
    void Start()
    {
        selfCamera = gameObject.GetComponent<Camera>();
        selfCamera.clearFlags = CameraClearFlags.Depth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

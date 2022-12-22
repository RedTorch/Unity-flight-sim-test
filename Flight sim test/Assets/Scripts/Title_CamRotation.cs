using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_CamRotation : MonoBehaviour
{
    [Tooltip("In degrees per second")]
    public float RotationSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,RotationSpeed*Time.deltaTime,0f);
    }
}

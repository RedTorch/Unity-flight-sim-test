using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Cam3p;

    public bool IsFPCam = false; // default is third person camera

    // Start is called before the first frame update
    void Start()
    {
        SetIsFPCam(IsFPCam); //refreshes configuration if ISFPCam was set in the editor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsFPCam(bool value) {
        if(value == true) {
            IsFPCam = true;
            Cam3p.enabled = false;
        }
        else {
            IsFPCam = false;
            Cam3p.enabled = true;
        }
    }

    public bool IsFirstPersonCamera() {
        return IsFPCam;
    }

    public Camera GetMainCam() {
        return Cam3p;
    }
}

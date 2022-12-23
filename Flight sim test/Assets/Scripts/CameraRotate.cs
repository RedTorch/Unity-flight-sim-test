using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // Below: rotate camera (child of plane) when plane is turning
    public PlaneController PC;
    [Tooltip("The amount of slerp angle per second. Default: 2f")]
    public float smooth = 2f;
    [Tooltip("The magnitude of tilt angle relative to y input. Default: 10f")]
    public float tiltAngle = 10f;
    void LateUpdate () {
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height/2) * PC.ControlCircleSize;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        float deltMpx = Mathf.Clamp((mousePos.x-centerX)/(controlCircleRadius*2),-1f,1f);
        float deltMpy = Mathf.Clamp((mousePos.y-centerY)/(controlCircleRadius*2),-1f,1f);

        float tiltAroundZ = deltMpx * tiltAngle;
        float tiltAroundX = deltMpy * -1f * tiltAngle;
        // float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
        Quaternion target = Quaternion.Euler (tiltAroundX, 0f, tiltAroundZ);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
    }
}
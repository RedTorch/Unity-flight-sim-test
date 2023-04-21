using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [Tooltip("The amount of slerp angle per second. Default: 2f")]
    public float smooth = 2f;
    [Tooltip("The magnitude of tilt angle relative to y input. Default: 10f")]
    public float tiltAngle = 5f;

    public void UpdateAngles(float deltMpx, float deltMpy, bool isFreelookMode = false) {
        if(isFreelookMode) {
            // do the freelook mode
            return;
        }
        float tiltAroundZ = deltMpx * tiltAngle;
        float tiltAroundX = deltMpy * -1f * tiltAngle * 0.8f;
        // float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
        Quaternion target = Quaternion.Euler (tiltAroundX, 0f, tiltAroundZ);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
    }
}

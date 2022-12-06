using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // this script can be used to add plane-like functionality to any object that has a rigidbody

    public GameObject CircleUI;

    public float SpeedInMetersPerSecond = 1f;
    public float RollSpeed = 2f;
    public float PitchSpeed = 2f;
    public float YawSpeed = 0.5f;
    [Tooltip("0 < x. Responsiveness of roll controls, in radius distance per second.")]
    public float MpxEaseSpeed = 2f;
    [Tooltip("0 < x. Responsiveness of pitch controls, in radius distance per second.")]
    public float MpyEaseSpeed = 1f;
    [Tooltip("0 < x. Responsiveness of thrust controls, in <delta normal --> max/min thrust> per second.")]
    public float ThrustEaseSpeed = 1f;
    [Tooltip("0 < x <= 1. Percent diameter of mouse control circle relative to height of screen.")]
    public float ControlCircleSize = 0.6f;

    private float mpx = 0f;
    private float mpy = 0f;
    private float wasd = 0f;
    private float thrustMult = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateThrustMult();
        UpdateMP();
        wasd = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * SpeedInMetersPerSecond * thrustMult;
        gameObject.transform.Rotate(mpy*PitchSpeed*Time.deltaTime,wasd*YawSpeed*Time.deltaTime,-1f*mpx*RollSpeed*Time.deltaTime);
    }

    void UpdateMP() {
        // It is assumed that the screen width will be greater than the height;
        // if not, the horizontal edges of the circle may be cut off.
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height/2) * ControlCircleSize;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        mpx = Mathf.Lerp(mpx,Mathf.Clamp((mousePos.x-centerX)/controlCircleRadius,-1f,1f),MpxEaseSpeed*Time.deltaTime);
        mpy = Mathf.Lerp(mpy,Mathf.Clamp((mousePos.y-centerY)/controlCircleRadius,-1f,1f),MpyEaseSpeed*Time.deltaTime);

        // Below: update size of the circle UI
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.height * ControlCircleSize);
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * ControlCircleSize);
    }

    void UpdateThrustMult() {
        float vin = Input.GetAxis("Vertical");
        if(vin == 0) {
            if(thrustMult>1) {
                thrustMult -= ThrustEaseSpeed * Time.deltaTime;
            }
            else if(thrustMult<1) {
                thrustMult += ThrustEaseSpeed * 0.5f * Time.deltaTime;
            }
        }
        else if(vin > 0) {
            thrustMult += ThrustEaseSpeed * Time.deltaTime;
        }
        else if(vin < 0) {
            thrustMult -= ThrustEaseSpeed * 0.5f * Time.deltaTime;
        }
        thrustMult = Mathf.Clamp(thrustMult,0.5f,2f);
    }
}
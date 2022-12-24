using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // this script can be used to add plane-like functionality to any object that has a rigidbody

    public CameraManager CamManager;

    public CameraRotate CamRotate;
    public PlayerInput pInput;

    [Tooltip("For reference, the cruise speed of an F-22 is 545 meters per second --> 90 m/s at the 1:6 scale of the player model. The top speed, on the other hand, is 670 / 6 --> ~112 m/s which should be considered the maximum plausible speed assuming a modern fighter jet.")]
    public float SpeedInMetersPerSecond = 1f;
    public float RollSpeed = 2f;
    public float PitchSpeed = 2f;
    public float YawSpeed = 0.5f;
    [Tooltip("0 < x. Responsiveness of roll controls, in radius distance per second.")]
    public float ThrustEaseSpeed = 1f;
    [Tooltip("0 < x <= 1. Percent diameter of mouse control circle relative to height of screen.")]
    public float DefaultFov;
    public bool EnhanceMouseControlScaling = true;

    [SerializeField] private AnimationCurve mpAnimCurve;
    [SerializeField] private AnimationCurve thrustAnimCurve;
    [SerializeField] private AnimationCurve ThrustEaseAnimCurve;

    private float mpx = 0f;
    private float mpy = 0f;
    private float thrustEaser = 0f; //default 0
    private float thrustMult = 1f; //default 1

    private float SecondsUntilEnabled = 2f;

    private bool IsControlsEnabled = false;

    private float ControlCircleSize = 0.8f; // This is hacky, remove later when player input is fully isolated

    void Start()
    {
        DefaultFov = CamManager.GetMainCam().fieldOfView;
    }

    void Update()
    {
        if(IsControlsEnabled) {
            float vin = pInput.GetVals()[3];
            thrustEaser = Mathf.Lerp(thrustEaser,vin,ThrustEaseSpeed*ThrustEaseAnimCurve.Evaluate(Mathf.Abs(vin-thrustEaser))*Time.deltaTime);
            thrustMult = thrustAnimCurve.Evaluate(thrustEaser);
            CamManager.GetMainCam().fieldOfView = DefaultFov + (thrustMult - 1f)*20f;
            mpx = pInput.GetVals()[0];
            mpy = pInput.GetVals()[1];
            gameObject.transform.Rotate(mpy*PitchSpeed*Time.deltaTime,pInput.GetVals()[2]*YawSpeed*Time.deltaTime,-1f*mpx*RollSpeed*Time.deltaTime);
        }
        else {
            SecondsUntilEnabled -= Time.deltaTime;
            if(SecondsUntilEnabled <= 0) {
                IsControlsEnabled = true;
            }
        }
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * SpeedInMetersPerSecond * thrustMult;
        CamRotate.UpdateAngles(mpx,mpy);
    }

    public bool GetIsControlsEnabled() {
        return IsControlsEnabled;
    }

    public void SetIsControlsEnabled(bool val) {
        IsControlsEnabled = val;
    }

    public float GetVelocity() {
        return SpeedInMetersPerSecond * thrustMult;
    }
}
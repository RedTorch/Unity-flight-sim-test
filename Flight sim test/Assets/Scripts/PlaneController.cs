using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // this script can be used to add plane-like functionality to any object that has a rigidbody

    public GameObject CircleUI;

    public Canvas MainUiCanvas;

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
    public float DefaultFov;

    public bool EnhanceMouseControlScaling = true;

    [SerializeField] private AnimationCurve mpAnimCurve;
    [SerializeField] private AnimationCurve thrustAnimCurve;
    [SerializeField] private AnimationCurve ThrustEaseAnimCurve;

    private float mpx = 0f;
    private float mpy = 0f;
    private float thrustEaser = 0f;
    private float thrustMult = 1f;

    void Start()
    {
        DefaultFov = Camera.main.fieldOfView;
    }

    void Update()
    {
        UpdateThrustMult();
        UpdateMP();
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * SpeedInMetersPerSecond * thrustMult;
        gameObject.transform.Rotate(mpy*PitchSpeed*Time.deltaTime,Input.GetAxis("Horizontal")*YawSpeed*Time.deltaTime,-1f*mpx*RollSpeed*Time.deltaTime);
        //canvas = FindObjectOfType<Canvas>();
    }

    void UpdateMP() {
        // It is assumed that the screen width will be greater than the height;
        // if not, the horizontal edges of the circle may be cut off.
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height/2) * ControlCircleSize;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        float deltaEase = MpxEaseSpeed*Time.deltaTime;
        float deltMpx = Mathf.Clamp((mousePos.x-centerX)/(controlCircleRadius*2),-1f,1f);
        float deltMpy = Mathf.Clamp((mousePos.y-centerY)/(controlCircleRadius*2),-1f,1f);
        if(EnhanceMouseControlScaling) {
            mpx = Mathf.Lerp(mpx,Mathf.Sign(deltMpx)*mpAnimCurve.Evaluate(Mathf.Abs(deltMpx)),deltaEase);
            mpy = Mathf.Lerp(mpy,Mathf.Sign(deltMpy)*mpAnimCurve.Evaluate(Mathf.Abs(deltMpy)),deltaEase);
        }
        else {
            mpx = Mathf.Lerp(mpx,deltMpx,deltaEase);
            mpy = Mathf.Lerp(mpy,deltMpy,deltaEase);
        }

        // Below: update size of the circle UI
        float h = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, h * ControlCircleSize);
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h * ControlCircleSize);
        print("screen height: " + Screen.height);
    }

    void UpdateThrustMult() {
        float vin = Input.GetAxis("Vertical");
        thrustEaser = Mathf.Lerp(thrustEaser,vin,ThrustEaseSpeed*ThrustEaseAnimCurve.Evaluate(Mathf.Abs(vin-thrustEaser))*Time.deltaTime);
        thrustMult = thrustAnimCurve.Evaluate(thrustEaser);
        Camera.main.fieldOfView = DefaultFov + (thrustMult - 1f)*20f;
    }
}
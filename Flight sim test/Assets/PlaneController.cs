using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // this script can be used to add plane-like functionality to any object that has a rigidbody

    public float Speed = 1f;
    public float RollSpeed = 2f;
    public float PitchSpeed = 2f;
    [Tooltip("0 < x. Responsiveness of roll controls.")]
    public float MpxEaseSpeed = 2f;
    [Tooltip("0 < x. Responsiveness of pitch controls.")]
    public float MpyEaseSpeed = 1f;
    [Tooltip("0 < x <= 1. Size of mouse control circle relative to height of screen.")]
    public float ControlCircleSize = 0.6f;

    private float mpx = 0f;
    private float mpy = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        UpdateMP();
        gameObject.transform.Rotate(mpy*PitchSpeed*Time.deltaTime,0f,-1f*mpx*RollSpeed*Time.deltaTime);
    }

    void UpdateMP() {
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height/2) * ControlCircleSize;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        mpx = Mathf.Lerp(mpx,Mathf.Clamp((mousePos.x-centerX)/controlCircleRadius,-1f,1f),MpxEaseSpeed*Time.deltaTime);
        mpy = Mathf.Lerp(mpy,Mathf.Clamp((mousePos.y-centerY)/controlCircleRadius,-1f,1f),MpyEaseSpeed*Time.deltaTime);
    }
}

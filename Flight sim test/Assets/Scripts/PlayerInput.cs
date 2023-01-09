using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public MainUIManager uiman;
    public MissileLockController mlcon;
    public float ControlCircleSize = 0.8f;
    public float MouseEaseSpeed = 8f;

    private float mpx, mpy;

    // Start is called before the first frame update
    void Start()
    {
        SetControlCircleSize(ControlCircleSize);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMissileLockController();
    }

    private float[] GetInputMouse() {
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height) * ControlCircleSize / 2f;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        float deltaEase = MouseEaseSpeed*Time.deltaTime;
        float deltMpx = Mathf.Clamp((mousePos.x-centerX)/(controlCircleRadius),-1f,1f);
        float deltMpy = Mathf.Clamp((mousePos.y-centerY)/(controlCircleRadius),-1f,1f);
        mpx = Mathf.Lerp(mpx,deltMpx,deltaEase);
        mpy = Mathf.Lerp(mpy,deltMpy,deltaEase);
        float[] ret = {Mathf.Floor(mpx*100f)/100f, Mathf.Floor(mpy*100f)/100f};
        return ret;
    }

    private float[] GetInputWasd() {
        float[] ret = {Mathf.Floor(Input.GetAxis("Horizontal")*100f)/100f, Mathf.Floor(Input.GetAxis("Vertical")*100f)/100f};
        return ret;
    }

    public float[] GetVals() {
        float[] mouse = GetInputMouse();
        float[] wasd = GetInputWasd();
        float[] ret = {mouse[0], mouse[1], wasd[0], wasd[1]};
        return ret;
    }

    public float GetControlCircleSize() {
        return ControlCircleSize;
    }

    public void SetControlCircleSize(float value) {
        if(value <= 0 || value > 1 || value == 0.6f) {
            print("invalid control circle size set: " + value);
            return;
        }
        print("setting control circle: " + value);
        ControlCircleSize = value;
        uiman.UpdateControlCircleSprite(ControlCircleSize);
    }

    private void UpdateMissileLockController() {
        if(Input.GetButtonDown("Fire2")) {
            // mlcon.FireMissile();
        }
    }
}

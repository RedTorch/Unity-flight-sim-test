using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject CircleUI;
    public Canvas MainUiCanvas;
    
    public float ControlCircleSize = 0.8f;

    public float MouseEaseSpeed = 8f;

    private float mpx, mpy;

    // Start is called before the first frame update
    void Start()
    {
        SetControlCircleSize();
    }

    // Update is called once per frame
    void Update()
    {
        SetControlCircleSize();
    }

    private float[] GetInputMouse() {
        Vector3 mousePos = Input.mousePosition;
        float controlCircleRadius = (Screen.height/2) * ControlCircleSize;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        float deltaEase = MouseEaseSpeed*Time.deltaTime;
        float deltMpx = Mathf.Clamp((mousePos.x-centerX)/(controlCircleRadius*2),-1f,1f);
        float deltMpy = Mathf.Clamp((mousePos.y-centerY)/(controlCircleRadius*2),-1f,1f);
        mpx = Mathf.Lerp(mpx,deltMpx,deltaEase);
        mpy = Mathf.Lerp(mpy,deltMpy,deltaEase);
        float[] ret = {mpx, mpy};
        return ret;
    }

    private float[] GetInputWasd() {
        float[] ret = {Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")};
        return ret;
    }

    public float[] GetVals() {
        float[] mouse = GetInputMouse();
        float[] wasd = GetInputWasd();
        float[] ret = {mouse[0], mouse[1], wasd[0], wasd[1]};
        return ret;
    }

    public void SetControlCircleSize() {
        // Below: update size of the circle UI
        float h = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, h * ControlCircleSize);
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h * ControlCircleSize);
    }
}

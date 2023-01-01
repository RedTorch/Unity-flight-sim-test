using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public TMP_Text cornerText;
    public Image dialogueBackground;
    public RectTransform reticle;
    public RectTransform cursor;
    public Image healthbar;

    public bool isBackgroundEnabled = true;
    public TMP_Text dialogue;
    public TMP_Text dialogueSpeakerLabel;
    public GameObject CircleUI;
    public Canvas MainUiCanvas;

    public HealthManager hm;
    public PlayerInput pm;


    // Start is called before the first frame update
    void Start()
    {
        // setSpeakerAndMessage("Bad Guy!", "This is a message", "enemy");
        setSpeakerAndMessage();
        dialogueBackground.enabled = isBackgroundEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        cornerText.text = GetCornerStats();
        SetMousePos();
    }

    public void UpdateControlCircleSprite(float size) {
        // Below: update size of the circle UI
        print("updating control circle sprite: size = " + size);
        float hh = Screen.height;
        float h = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, h * size);
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h * size);
        print("size set with Screen.height = " + hh + ", MainUiCanvas.GetComponent<RectTransform>().rect.height = " + h);
    }

    public void setSpeakerAndMessage(string speaker = "", string message = "", string isFriendly = "friendly") {
        if(speaker=="" && message=="") {
            dialogueBackground.enabled = false;
            dialogueSpeakerLabel.text = "";
            dialogue.text = "";
            return;
        }
        dialogueBackground.enabled = isBackgroundEnabled;
        dialogueSpeakerLabel.text = speaker;
        dialogue.text = message;
        if(isFriendly == "enemy") {
            dialogueSpeakerLabel.color = new Color(1f,0f,0f,1f);
        }
        else {
            dialogueSpeakerLabel.color = new Color(0f,0f,1f,1f);
        }
    }

    public void UpdateCornerText(string val) {
        cornerText.text = val;
    }

    public string GetCornerStats() {
        float hp = hm.GetHealth();
        float mpx = pm.GetVals()[0];
        float mpy = pm.GetVals()[1];
        float ad = pm.GetVals()[2];
        float ws = pm.GetVals()[3];
        string ret = "HP: " + hp + "\nMousePos (" +mpx+ ", " +mpy+ ")" + "\nWASD (" +ad+ ", " +ws+ ")";
        ret = ret + "\nRefDisplaySize: (" + MainUiCanvas.GetComponent<RectTransform>().rect.width + ", " + MainUiCanvas.GetComponent<RectTransform>().rect.height + ")";
        ret = ret + "\nActualDisplaySize: (" + Screen.width + ", " + Screen.height + ")";
        // print(ret);
        return ret;
    }

    public void SetReticlePos(Vector3 pos) {
        reticle.anchoredPosition = WorldToScreenPos(pos);
    }

    public void SetMousePos() {
        cursor.anchoredPosition = ActualToReferenceScreenPos(Input.mousePosition.x,Input.mousePosition.y);
    }

    public Vector2 WorldToScreenPos(Vector3 pos) { // given world position, returns position relative to center screen
        Vector3 convpos = Camera.main.WorldToScreenPoint(pos);
        return ActualToReferenceScreenPos(convpos.x,convpos.y);
    }

    public Vector2 ActualToReferenceScreenPos(float x, float y) {
        float refWidth = MainUiCanvas.GetComponent<RectTransform>().rect.width;
        float refHeight = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        float actWidth = Screen.width;
        float actHeight = Screen.height;
        float xcomp = refWidth*((x/actWidth)-0.5f);
        float ycomp = refHeight*((y/actHeight)-0.5f);
        Vector2 ret = new Vector2(xcomp,ycomp);
        return ret;
    }

    public void UpdateHealthBar(float percent) {
        healthbar.fillAmount = percent;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = false;
    }
}

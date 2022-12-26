using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public TMP_Text cornerText;

    public Image dialogueBackground;
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
    }

    // Update is called once per frame
    void Update()
    {
        cornerText.text = GetCornerStats();
    }

    public void UpdateControlCircleSprite(float size) {
        // Below: update size of the circle UI
        float h = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, h * size);
        CircleUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h * size);
    }

    public void setSpeakerAndMessage(string speaker = "", string message = "", string speakerType = "friendly") {
        if(speaker=="" && message=="") {
            dialogueBackground.enabled = false;
        }
        dialogueBackground.enabled = true;
        dialogueSpeakerLabel.text = speaker;
        dialogue.text = message;
        if(speakerType == "enemy") {
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
        // print(ret);
        return ret;
    }
}

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
    public MissileLockController mlcon;

    public bool isBackgroundEnabled = true;
    public TMP_Text dialogue;
    public TMP_Text dialogueSpeakerLabel;
    public GameObject CircleUI;
    public Canvas MainUiCanvas;

    public HealthManager hm;
    public PlayerInput pm;

    public List<Vector2> lockingTargetsPos = new List<Vector2>();
    public List<Vector2> lockedTargetsPos = new List<Vector2>();
    public GameObject lockingTargetPrefab;
    public Transform lockingTargetBoxRoot;
    private GameObject[] targetPrefabs = new GameObject[200];

    public GameObject gameOverMenu;
    public TMP_Text gameOverText;
    
    public TMP_Text scoreText;

    public GameObject player;

    private int kills = 0;
    private int wave = 0;
    private int score = 0;

    public Image[] missileIndicators;


    // Start is called before the first frame update
    void Start()
    {
        // setSpeakerAndMessage("Bad Guy!", "This is a message", "enemy");
        setSpeakerAndMessage();
        dialogueBackground.enabled = isBackgroundEnabled;
        for(int i = 0; i < targetPrefabs.Length; i++) {
            GameObject inst = Instantiate(lockingTargetPrefab, transform.position, transform.rotation, lockingTargetBoxRoot);
            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,0f);
            inst.SetActive(false);
            inst.GetComponent<TargetingBoxScript>().SetPlayer(player);
            targetPrefabs[i] = inst;
        }
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cornerText.text = GetCornerStats();
        UpdateTargetBoxes();
        //
        SetMousePos();
        UpdateScore();
        UpdateMissileIndicators();
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

    public Vector2 ActualToReferenceScreenPos(float x, float y, bool assumeOriginIsAtCenter = true) {
        float refWidth = MainUiCanvas.GetComponent<RectTransform>().rect.width;
        float refHeight = MainUiCanvas.GetComponent<RectTransform>().rect.height;
        float actWidth = Screen.width;
        float actHeight = Screen.height;
        if(!assumeOriginIsAtCenter) {
            Vector2 rett = new Vector2(x*refWidth/actWidth,y*refHeight/actHeight);
            return rett;
        }
        float xcomp = refWidth*((x/actWidth)-0.5f);
        float ycomp = refHeight*((y/actHeight)-0.5f);
        Vector2 ret = new Vector2(xcomp,ycomp);
        return ret;
    }

    public void UpdateHealthBar(float percent) {
        healthbar.fillAmount = percent;
    }

    public void UpdateTargetBoxes() {
        if(!player) {
            return;
        }
        List<GameObject> locking = mlcon.GetLockingTargets();
        List<GameObject> locked = mlcon.GetLockedTargets();
        for(int i = 0; i < targetPrefabs.Length; i++) {
            if(i<locking.Count) {
                GameObject curr = locking[i];
                targetPrefabs[i].GetComponent<TargetingBoxScript>().SetFocusedObject(curr,1);
                targetPrefabs[i].GetComponent<TargetingBoxScript>().SetFill(mlcon.GetLockPercent(curr));
                targetPrefabs[i].GetComponent<RectTransform>().anchoredPosition = WorldToScreenPos(curr.transform.position);
                targetPrefabs[i].SetActive(true);
            }
            else if(i<(locking.Count + locked.Count)) {
                if(i<locking.Count + mlcon.getMaxTargets()) {
                    GameObject curr = locked[i-locking.Count];
                    targetPrefabs[i].GetComponent<TargetingBoxScript>().SetFocusedObject(curr,2);
                    targetPrefabs[i].GetComponent<RectTransform>().anchoredPosition = WorldToScreenPos(curr.transform.position);
                    targetPrefabs[i].SetActive(true);
                }
                else {
                    GameObject curr = locked[i-locking.Count];
                    targetPrefabs[i].GetComponent<TargetingBoxScript>().SetFocusedObject(curr,1);
                    targetPrefabs[i].GetComponent<TargetingBoxScript>().SetFill(mlcon.GetLockPercent(curr));
                    targetPrefabs[i].GetComponent<RectTransform>().anchoredPosition = WorldToScreenPos(curr.transform.position);
                    targetPrefabs[i].SetActive(true);
                }
            }
            else if(targetPrefabs[i].activeInHierarchy) {
                targetPrefabs[i].SetActive(false);
            }
        }
    }

    public void GameOverMenu() {
        gameOverText.text = wave + "\n" + (kills).ToString() + "\n" + score;
        reticle.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
        lockingTargetBoxRoot.gameObject.SetActive(false);
    }

    private void UpdateScore() {
        score = (50*kills) + (100*wave);
        scoreText.text = (score).ToString();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = false;
    }

    public void addKill() {
        kills++;
    }

    public void UpdateMissileIndicators() {
        // get status
        float[] vals = mlcon.getMissileStatus();
        // display status
        for(int i=0; i<missileIndicators.Length; i++) {
            missileIndicators[i].fillAmount = Mathf.Clamp(vals[i],0f,1f);
        }
    }
}
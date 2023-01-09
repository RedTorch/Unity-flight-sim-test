using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetingBoxScript : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private TMP_Text distance;
    [SerializeField] private TMP_Text nametag;
    [SerializeField] private GameObject boxSpriteRoot;
    [SerializeField] private GameObject textRoot;

    [SerializeField] private Image sprite_box;
    [SerializeField] private Image sprite_diamond;

    private GameObject player;
    private GameObject focusedObject;

    private float fillPercent = 0f;
    // Start is called before the first frame update
    void Start()
    {
        textRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(focusedObject != null) {
            float dist = Mathf.Floor(Vector3.Distance(player.transform.position,focusedObject.transform.position));
            distance.text = dist + "m";
            SetScale(500f/(dist));
        }
    }

    public void SetPlayer(GameObject p) {
        player = p;
    }

    public void SetFill(float f) {
        fillPercent = Mathf.Clamp(f,0f,1f);
    }

    public void SetFocusedObject(GameObject obj, int state) {
        focusedObject = obj;
        nametag.text = obj.name;
        SetState(state);
        float dist = Mathf.Floor(Vector3.Distance(player.transform.position,focusedObject.transform.position));
        distance.text = dist + "m";
        SetScale(500f/(dist));
    }

    private void SetScale(float newScale) {
        scale = Mathf.Clamp(newScale, 0.5f, 1f);
        // scale = 1f;
        boxSpriteRoot.GetComponent<RectTransform>().localScale = new Vector3(scale * 0.5f, scale * 0.5f, 1f);
        textRoot.GetComponent<RectTransform>().localScale = new Vector3(2f/scale, 2f/scale, 1f);
    }

    public void SetState(int state) {
        if(state == 0) {
            // normal sprite (transparent?)
            sprite_box.color = new Color(1f, 1f, 1f, 0.5f);
            sprite_box.enabled = true;
            sprite_diamond.enabled = false;
        }
        else if(state == 1) {
            // filling sprite
            sprite_box.color = new Color(1f, 1f, 1f, 1f);
            sprite_diamond.color = new Color(1f, 1f, 1f, 1f);
            sprite_box.enabled = true;
            sprite_diamond.enabled = true;
            sprite_diamond.fillAmount = fillPercent;
        }
        else if(state == 2) {
            // locked sprite
            sprite_box.color = new Color(1f, 0f, 0f, 1f);
            sprite_diamond.color = new Color(1f, 0f, 0f, 1f);
            sprite_box.enabled = true;
            sprite_diamond.enabled = true;
            sprite_diamond.fillAmount = 1f;
        }
    }

    public void Reset() {
        textRoot.SetActive(false);
        sprite_box.enabled = true;
        sprite_diamond.enabled = false;
    }
}

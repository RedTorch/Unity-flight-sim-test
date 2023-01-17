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

    [SerializeField] private Color color_visible;
    [SerializeField] private Color color_locking;
    [SerializeField] private Color color_locking_diamond;
    [SerializeField] private Color color_locked;

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
        if(focusedObject != null && player != null) {
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
        if(obj == null || player == null) {
            return;
        }
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
            sprite_box.color = color_visible;
            sprite_box.enabled = true;
            sprite_box.fillAmount = 1f;
            sprite_diamond.enabled = false;
        }
        else if(state == 1) {
            // filling sprite
            sprite_box.color = color_locking;
            sprite_diamond.color = color_locking_diamond;
            sprite_box.enabled = true;
            sprite_box.fillAmount = fillPercent;
            sprite_diamond.enabled = true;
            sprite_diamond.fillAmount = 1f;
        }
        else if(state == 2) {
            // locked sprite
            sprite_box.color = color_locked;
            sprite_diamond.color = color_locked;
            sprite_box.enabled = true;
            sprite_box.fillAmount = 1f;
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

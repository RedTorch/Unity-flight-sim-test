using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookAtPlayer : MonoBehaviour
{
    private Transform lookCam;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text dist;
    // Start is called before the first frame update
    void Start()
    {
        lookCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookCam.position);
        float distance = Vector3.Distance(transform.position,lookCam.position);
        if(distance <= 800f) {
            dist.text = Mathf.Floor(distance) + "m";
            img.enabled = true;
            dist.enabled = true;
        }
        else {
            img.enabled = false;
            dist.enabled = false;
        }
    }
}

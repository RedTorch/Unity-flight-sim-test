using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookAtPlayer : MonoBehaviour
{
    private Transform lookCam;
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
        dist.text = Mathf.Floor(Vector3.Distance(transform.position,lookCam.position)) + "m";
    }
}

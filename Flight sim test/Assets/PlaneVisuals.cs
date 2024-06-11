using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For rendering trails and other worldspace VFX on player-controlled and AI aircraft.

public class PlaneVisuals : MonoBehaviour
{

    [SerializeField] private TrailRenderer[] trails;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(TrailRenderer trail in trails) {
            //trail.
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_CloudMove : MonoBehaviour
{
    [SerializeField] private float MetersPerSec;
    [SerializeField] private float FogDensity;
    [SerializeField] private float DefaultAlpha;
    [SerializeField] private float MaxDistance;
    private Renderer renderer;
    private Transform mct;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        mct = Camera.main.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f,0f,-MetersPerSec*Time.deltaTime));
        // float distance = Vector3.Distance(transform.position,mct.position);
        // float newAlpha = DefaultAlpha * (MaxDistance-distance) / MaxDistance;
        // newAlpha = Mathf.Clamp(newAlpha,0f,1f);
        // // renderer.material.color.a = Mathf.Clamp(newAlpha,0f,1f);
        // Color theColorToAdjust = renderer.material.color;
        // theColorToAdjust.a = newAlpha;
        // renderer.material.color = theColorToAdjust;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_CloudGen : MonoBehaviour
{
    [SerializeField] private GameObject Cloud;
    // [SerializeField] private float CloudsPerSecond;
    [SerializeField] private int TotalClouds;
    [SerializeField] private float Xoffset;
    [SerializeField] private float Yoffset;
    [SerializeField] private float Zoffset;
    private float CalcFreq;
    private float CurrTime = 0;
    private Vector3 Offset;
    private GameObject[] Clouds;
    // Start is called before the first frame update
    void Start()
    {
        // CalcFreq = 1/CloudsPerSecond;
        Clouds = new GameObject[TotalClouds];
        for(int i = 0; i < TotalClouds; i++) {
            Offset = new Vector3(Random.Range(-Xoffset,Xoffset),Random.Range(-Yoffset,Yoffset),Random.Range(-Zoffset,Zoffset));
            Clouds[i] = Instantiate(Cloud,transform.position + Offset,Quaternion.identity,gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // CurrTime += Time.deltaTime;
        // if(CurrTime>CalcFreq) {
        //     Offset = new Vector3(Random.Range(-Xoffset,Xoffset),Random.Range(-Yoffset,Yoffset),Random.Range(-Zoffset,Zoffset));
        //     Instantiate(Cloud,transform.position + Offset,Quaternion.identity);
        //     CurrTime = CurrTime%CalcFreq;
        // }
        for(int i = 0; i < TotalClouds; i++) {
            GameObject curr = Clouds[i];
            if(curr.transform.position.z < -(Zoffset)) {
                curr.transform.Translate(new Vector3(0f,0f,2*Zoffset));
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarFollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) {
            return;
        }
        transform.position = new Vector3(target.position.x, 10000f + target.position.y, target.position.z);
    }
}

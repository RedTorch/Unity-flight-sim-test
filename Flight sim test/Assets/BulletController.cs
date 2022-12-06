using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speedInMetersPerSecond;
    public float maxDistance = 120f;
    private float totalDistTraveled = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardDistance = speedInMetersPerSecond * Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, forwardDistance)) {
            Destroy(gameObject);
        }
        else if(totalDistTraveled >= maxDistance) {
            Destroy(gameObject);
        }
        else {
            transform.Translate(Vector3.forward * forwardDistance);
            totalDistTraveled += forwardDistance;
        }
    }
}
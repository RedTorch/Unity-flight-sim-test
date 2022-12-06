using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Target;
    public float RPM = 120f;
    public float DetectRangeInMeters = 200f;
    private bool isFiring = false;
    private float FireIntervalInSeconds;
    private float fireCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        FireIntervalInSeconds = 60 / RPM;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
        else if(isFiring && Vector3.Distance(transform.position,Target.transform.position) <= DetectRangeInMeters) {
            Instantiate(Bullet,transform.position + new Vector3(0f,1f,0f),transform.rotation);
            fireCooldown += FireIntervalInSeconds;
        }
    }
}

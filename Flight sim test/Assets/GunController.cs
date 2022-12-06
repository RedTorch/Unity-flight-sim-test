using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;
    public float RPM = 120f;
    private float FireIntervalInSeconds;
    private float fireCooldown = 0f;

    void Start() 
    {
        FireIntervalInSeconds = 60 / RPM;
    }   

    void Update() 
    {
        if(fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
        else if(Input.GetButton("Fire1")) {
            Instantiate(Bullet,transform.position,transform.rotation);
            fireCooldown += FireIntervalInSeconds;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;
    public float RPM = 600f;
    private float FireIntervalInSeconds;
    private float fireCooldown = 0f;
    public float SpreadInDegs = 0.2f;

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
            GameObject b = Instantiate(Bullet,transform.position,transform.rotation);
            Vector3 spreadVec = new Vector3(Random.Range(0f, SpreadInDegs), Random.Range(0f,SpreadInDegs), 0);
            b.transform.Rotate(spreadVec);
            fireCooldown += FireIntervalInSeconds;
            // CameraShake.Shake(0.25f,0.25f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockController : MonoBehaviour
{
    private bool MissileTracking = false;
    private float LockRange = 2000f;
    private float LockAngle = 20f;


    // hacky code alert below!

    public GameObject Bullet;
    public float RPM = 600f;
    private float FireIntervalInSeconds;
    private float fireCooldown = 0f;
    public float SpreadInDegs = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        FireIntervalInSeconds = 60 / RPM;
    }

    // Update is called once per frame
    void Update()
    {
        TryLock(GameObject.FindWithTag("Enemy"));
    }

    void TryLock(GameObject target) {
        MissileTracking = false;
        Quaternion targetAngle = Quaternion.identity;
        Vector3 delta = target.transform.position - transform.position;
        if (delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle) {
            MissileTracking = true;
            targetAngle = Quaternion.LookRotation(delta);
        }
        
        if(fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
        else if(MissileTracking) {
            GameObject b = Instantiate(Bullet,transform.position,targetAngle);
            Vector3 spreadVec = new Vector3(Random.Range(0f, SpreadInDegs), Random.Range(0f,SpreadInDegs), 0);
            b.transform.Rotate(spreadVec);
            fireCooldown += FireIntervalInSeconds;
            // CameraShake.Shake(0.25f,0.25f);
        }
    }  

}

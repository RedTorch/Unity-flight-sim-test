using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversaryAI : MonoBehaviour
{
    public GunController gc;
    private Transform target;
    private Rigidbody myRb;
    private float turnSpeed = 0.001f;
    private float speed = 40f;
    private float currSpeed;
    Quaternion rotGoal;
    Vector3 direction;
    private float LockRange = 800f;
    private float LockAngle = 20f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform; //target selection; can expand upon later
        myRb = gameObject.GetComponent<Rigidbody>();
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        TrackTarget();
        AttemptFire();
        myRb.velocity = transform.forward * currSpeed;
    }

    void TrackTarget() {
        direction = (target.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }

    void AttemptFire() {
        Quaternion targetAngle = Quaternion.identity;
        Vector3 delta = target.position - transform.position;
        if(delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle) {
            gc.SetIsFiring(true);
            print("firing");
            currSpeed = speed * 0.6f;
        }
        else {
            gc.SetIsFiring( false);
            currSpeed = speed;
            print("not firing");
        }
    }
}

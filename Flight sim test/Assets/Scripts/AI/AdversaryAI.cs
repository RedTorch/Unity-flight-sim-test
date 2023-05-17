using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversaryAI : MonoBehaviour
{
    public GunController gc;
    private Transform target;
    private Rigidbody myRb;
    private float turnSpeed = 20f;
    private float speed = 40f;
    private float currSpeed;
    Quaternion rotGoal;
    Vector3 direction;
    private float LockRange = 800f;
    private float LockAngle = 20f;

    private float attackCooldown = 0f;
    private float attackCooldownMax = 2f;
    private bool canAttack = true;
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
        attackCooldown = Mathf.Clamp(attackCooldown-Time.deltaTime, 0f, 1f);
        if(attackCooldown==0f) {
            canAttack = true;
        }
        if(canAttack) {
            update_ManeuverTowards(target.position);
            AttemptFire();
        } else {
            // do nothing and keep traveling in a straight line
        }
        myRb.velocity = transform.forward * currSpeed;
    }

    private void update_ManeuverTowards(Vector3 targetPos)
    {
        Vector3 relativePosOfTarget = transform.InverseTransformPoint(targetPos);
        float turnIncr = turnSpeed*Time.deltaTime;
        if(relativePosOfTarget.x >= (turnIncr))
        {
            transform.Rotate(0f, 0f, -turnIncr);
        }
        else if(relativePosOfTarget.x <= -(turnIncr))
        {
            transform.Rotate(0f, 0f, turnIncr);
        }
        else if(relativePosOfTarget.y > turnIncr || relativePosOfTarget.z < 0f)
        {
            transform.Rotate(-turnIncr, 0f, 0f);
        }
    }

    void AttemptFire() {
        Quaternion targetAngle = Quaternion.identity;
        Vector3 delta = target.position - transform.position;
        if(delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle) {
            gc.SetIsFiring(true);
            print("firing");
            currSpeed = speed * 0.6f;
            attackCooldown += Time.deltaTime;
            if(attackCooldown >= attackCooldownMax) {
                canAttack = false;
            }
        }
        else {
            gc.SetIsFiring( false);
            currSpeed = speed;
            print("not firing");
        }
    }
}

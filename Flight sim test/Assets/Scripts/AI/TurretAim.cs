using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Target;
    public GameObject IndicatorCube;
    public float RPM = 120f;
    public float SpreadInDegs = 5f;
    public float DetectRangeInMeters = 200f;
    private bool isFiring = true;
    private float FireIntervalInSeconds;
    private float fireCooldown = 0f;
    private Transform[] Targets;
    // Start is called before the first frame update
    void Start()
    {
        FireIntervalInSeconds = 60 / RPM;
    }

    // Update is called once per frame
    void Update()
    {
        Target = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player"));
        if(Target == null) {
            return;
        }
        float dist = Vector3.Distance(transform.position,Target.transform.position);
        if(fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
        else if(isFiring && dist <= DetectRangeInMeters) {
            float deltaT = dist / Bullet.GetComponent<BulletController>().SpeedInMetersPerSecond;
            Vector3 targPositionOffset = Target.GetComponent<Rigidbody>().velocity * deltaT;
            Vector3 projectedTargetPosition = Target.transform.position + (targPositionOffset);
            // Instantiate(IndicatorCube,projectedTargetPosition,Quaternion.identity);
            GameObject b = Instantiate(Bullet,transform.position + new Vector3(0f,4f,0f),Quaternion.LookRotation(projectedTargetPosition - transform.position));
            Vector3 spreadVec = new Vector3(Random.Range(0f, SpreadInDegs), Random.Range(0f,SpreadInDegs), 0);
            b.transform.Rotate(spreadVec);
            fireCooldown += FireIntervalInSeconds;

        }
    }

    GameObject GetClosestEnemy (GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Target;
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
        if(fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
        else if(isFiring && Vector3.Distance(transform.position,Target.transform.position) <= DetectRangeInMeters) {
            GameObject b = Instantiate(Bullet,transform.position + new Vector3(0f,1f,0f),Quaternion.LookRotation(Target.transform.position-transform.position));
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

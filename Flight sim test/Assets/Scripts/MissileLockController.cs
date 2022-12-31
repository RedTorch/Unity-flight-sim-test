using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockController : MonoBehaviour
{
    private bool MissileTracking = false;
    [SerializeField] private float LockRange = 2000f;
    [SerializeField] private float LockAngle = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> targets = FindAllLockEligible();
        //TryLock(GameObject.FindWithTag("Enemy"));
        if(targets.Count > 0) {
            foreach(GameObject target in targets) {
                // print(target.transform.position);
            }
        }
    }

    public List<GameObject> FindAllLockEligible() {
        List<GameObject> toReturn = new List<GameObject>();
        Quaternion targetAngle = Quaternion.identity;
        Vector3 delta;
        foreach(GameObject target in GameObject.FindGameObjectsWithTag("Enemy")) {
            delta = target.transform.position - transform.position;
            if (delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle) {
                targetAngle = Quaternion.LookRotation(delta);
                toReturn.Add(target);
            }
        }
        return toReturn;
    }

}

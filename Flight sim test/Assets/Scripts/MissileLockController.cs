using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockController : MonoBehaviour
{
    private bool MissileTracking = false;
    private float TrackTime = 2f;
    [SerializeField] private float LockRange = 2000f;
    [SerializeField] private float LockAngle = 20f;

    private Dictionary<GameObject, float> IsLocking = new Dictionary<GameObject, float>();
    private List<GameObject> IsLocked = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateLockedList();
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

    public void UpdateLockedList() {
        Dictionary<GameObject, float> newIsLocking = new Dictionary<GameObject,float>();
        List<GameObject> targets = FindAllLockEligible();
        IsLocked = new List<GameObject>();
        if(targets.Count > 0) {
            foreach(GameObject target in targets) {
                if(IsLocking.ContainsKey(target)) {
                    float targetTime = IsLocking[target];
                    if(targetTime>=Time.time) {
                        // remove from isLocking
                        // add to IsLocked
                    }
                    else {
                        // add to new list with previous time
                    }
                }
                else {
                    // add to new list with time = 0
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockController : MonoBehaviour
{
    private string TargetedTag = "Enemy";
    private float LockTime = 2f;
    private float LockRange = 2000f;
    private float LockAngle = 20f;
    private bool isFiring = false;
    private float LockTimeMultiplier = 1f; // for speeding up or slowing down lock

    [SerializeField] private MainUIManager uiman;
    private Dictionary<GameObject, float> LockTable = new Dictionary<GameObject, float>();
    private List<GameObject> lockedTargets = new List<GameObject>();
    private List<GameObject> lockingTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(TargetedTag)) {
            LockTable.Add(target,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(TargetedTag)) {
            if(!LockTable.ContainsKey(target)) {
                LockTable.Add(target,0f);
            }
        }
        lockedTargets.Clear();
        lockingTargets.Clear();
        Dictionary<GameObject, float> readOnlyLockTable = LockTable;
        // GameObject[] ltkeys = new GameObject[LockTable.Count];
        GameObject[] ltkeys = new GameObject[LockTable.Count]; // ^ replaces above
        float[] ltvals = new float[LockTable.Count];
        LockTable.Keys.CopyTo(ltkeys, 0);
        LockTable.Values.CopyTo(ltvals, 0);
        for(int i = 0; i < ltkeys.Length; i++)
        {
            if(ltkeys[i] == null) {
                LockTable.Remove(ltkeys[i]);
            }
            // do something with entry.Value or entry.Key
            else if(isInLockingCone(ltkeys[i])) {
                if(ltvals[i] >= LockTime) {
                    // show locked reticle
                    lockedTargets.Add(ltkeys[i]);
                }
                else {
                    // show locking-in-progress reticle
                    lockingTargets.Add(ltkeys[i]);
                    LockTable[ltkeys[i]] = ltvals[i] + (LockTimeMultiplier * Time.deltaTime);
                }
            }
            else {
                LockTable[ltkeys[i]] = 0f;
            }
        }
        print("we have " + lockingTargets.Count + " locking and " + lockedTargets.Count + " locked out  of " + LockTable.Count + " elements");
        // uiman.UpdateTargetBoxes(lockingTargets,lockedTargets);
        // now LockTable has the time-in-lock of all targets
        // lockingTargets contains all locking targets
        // lockedTargets contains all locked targets
        if(isFiring) {
            isFiring = false;
            // Instantiate()
        }
    }

    public List<GameObject> GetLockingTargets() {
        return lockingTargets;
    }

    public List<GameObject> GetLockedTargets() {
        return lockedTargets;
    }

    public float GetLockTime(GameObject key) {
        return LockTable[key];
    }

    public bool isInLockingCone(GameObject target) {
        Vector3 delta = target.transform.position - transform.position;
        return (delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle);
    }

    public void FireMissile() {
        isFiring = true;
        print("Missile Fired");
    }
}
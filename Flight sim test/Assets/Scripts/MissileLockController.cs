using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLockController : MonoBehaviour
{
    private string TargetedTag = "Enemy";
    private float LockTime = 1f;
    private float LockRange = 2000f;
    private float LockAngle = 20f;
    private bool isFiring = false;
    private float LockTimeMultiplier = 1f; // for speeding up or slowing down lock

    [SerializeField] private MainUIManager uiman;
    private Dictionary<GameObject, float> LockTable = new Dictionary<GameObject, float>();
    private List<GameObject> lockedTargets = new List<GameObject>();
    private List<GameObject> lockingTargets = new List<GameObject>();

    private float[] missileBays = new float[] {0f,0f,0f,0f};
    private float secondsToReload = 4f;
    private float reloadSpeed = 1f;
    [SerializeField] private GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(TargetedTag)) {
            LockTable.Add(target,0f);
        }
        reloadSpeed = 1f/secondsToReload;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(TargetedTag)) {
            if(!LockTable.ContainsKey(target)) {
                LockTable.Add(target,0f);
            }
        }
        Dictionary<GameObject, float> readOnlyLockTable = LockTable;
        GameObject[] ltkeys = new GameObject[LockTable.Count];
        float[] ltvals = new float[LockTable.Count];
        LockTable.Keys.CopyTo(ltkeys, 0);
        LockTable.Values.CopyTo(ltvals, 0);
        lockedTargets.Clear();
        lockingTargets.Clear();
        for(int i = 0; i < ltkeys.Length; i++)
        {
            if(ltkeys[i] == null) {
                LockTable.Remove(ltkeys[i]);
            }
            // do something with entry.Value or entry.Key
            else if(isInLockingCone(ltkeys[i])) {
                if(ltvals[i] >= LockTime) {
                    lockedTargets.Add(ltkeys[i]);
                }
                else {
                    lockingTargets.Add(ltkeys[i]);
                    LockTable[ltkeys[i]] = ltvals[i] + (LockTimeMultiplier * Time.deltaTime);
                }
            }
            else {
                LockTable[ltkeys[i]] = 0f;
            }
        }
        if(lockedTargets.Count > getMaxTargets()) {
            // sort lockedTargets
            lockedTargets.Sort(compareTargetsByAngle);
            // now the first <maxTargets> items in the list are valid for targeting!
            // the rest are still locked, but cannot be attacked
            // maxTargets can be set to any number depending on missiles currently loaded/ targeting capacity of the missile itself.
        }
        // print("we have " + lockingTargets.Count + " locking and " + lockedTargets.Count + " locked out  of " + LockTable.Count + " elements");
        // now LockTable has the time-in-lock of all targets
        // lockingTargets contains all locking targets
        // lockedTargets contains all locked targets
        
        if(isFiring) {
            isFiring = false;
            foreach(GameObject target in lockedTargets) {
                if(getMaxTargets() > 0) {
                    fireSingleMissile(target);
                }
            }
        }

        for(int i = 0; i < missileBays.Length; i++) {
            if(missileBays[i] < 1f) {
                missileBays[i] = Mathf.Clamp(missileBays[i] + (reloadSpeed * Time.deltaTime),0f,1f);
            }
        }

        //reload missiles;
    }

    private int compareTargetsByAngle(GameObject go1, GameObject go2) {
        if(getConeAngleTo(go1) > getConeAngleTo(go2)) {
            return 1;
        }
        else if(getConeAngleTo(go1) < getConeAngleTo(go2)) {
            return -1;
        }
        return 0;
    }

    private float getConeAngleTo(GameObject target) {
        Vector3 delta = target.transform.position - transform.position;
        return Vector3.Angle(transform.forward, delta);
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

    public float GetLockPercent(GameObject key) {
        return Mathf.Clamp(LockTable[key]/LockTime,0f,1f);
    }

    public bool isInLockingCone(GameObject target) {
        Vector3 delta = target.transform.position - transform.position;
        return (delta.magnitude <= LockRange && Vector3.Angle(transform.forward, delta) <= LockAngle);
    }

    public void FireMissile() {
        isFiring = true;
        // the isFiring is evaluated next update of the controller;
    }

    public int getMaxTargets() {
        int ret = 0;
        foreach(float f in missileBays) {
            if(f >= 1f) {
                ret += 1;
            }
        }
        return ret;
    }

    public void fireSingleMissile(GameObject missileTarget) {
        for(int i = 0; i < missileBays.Length; i++) {
            if(missileBays[i] >= 1f) {
                missileBays[i] = 0f;
                GameObject newm = Instantiate(missilePrefab,transform.position,transform.rotation);
                newm.GetComponent<MissileController>().InstantiateVars(missileTarget, gameObject.tag);
                return;
            }
        }
        return;
    }

    public float[] getMissileStatus() {
        return missileBays;
    }

    public void updateLockTable() {
        LockTable.Clear();
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(TargetedTag)) {
            LockTable.Add(target,0f);
        }
    }
}
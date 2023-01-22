using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float turnSpeed = 90f;

    private float Damage = 20f;
    private float SpeedInMetersPerSecond = 400f;
    private float MaxDistance = 2000f;
    private float totalDistTraveled = 0f;
    private string parentTag = "Enemy";

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if(target) {
            // aim to target
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position-transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        float forwardDistance = SpeedInMetersPerSecond * Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, forwardDistance) && hit.collider.gameObject.tag != parentTag) {
            transform.position = hit.point;
            Explode();
        }
        else if(totalDistTraveled >= MaxDistance) {
            Explode();
        }
        else if(target && Vector3.Distance(transform.position,target.transform.position) < 3f) {
            Explode();
        }
        else if(target && Vector3.Angle(transform.forward,target.transform.position-transform.position) > 90f) {
            Explode();
        }
        else {
            transform.Translate(Vector3.forward * forwardDistance);
            totalDistTraveled += forwardDistance;
        }
    }

    public void InstantiateVars(GameObject targ, string ptag) {
        target = targ;
        parentTag = ptag;
    }

    private void Explode() {
        if(explosionPrefab) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        if(trailRenderer) {
            trailRenderer.transform.parent = null;
        }
        Collider[] colls = Physics.OverlapSphere(transform.position,4f);
        foreach(Collider col in colls) {
            if(gameObject.tag != parentTag && col.gameObject.GetComponent<DamageReceiver>() != null) {
                col.gameObject.GetComponent<DamageReceiver>().TakeDamage(Damage, parentTag);
            }
        }
        Destroy(gameObject);
    }
}
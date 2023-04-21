using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Damage = 1f;
    public float SpeedInMetersPerSecond;
    public float MaxDistance = 120f;
    private float totalDistTraveled = 0f;
    private string parentTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardDistance = SpeedInMetersPerSecond * Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, forwardDistance)) {
            GameObject target = hit.collider.gameObject;
            if(target.GetComponent<DamageReceiver>() != null) {
                target.GetComponent<DamageReceiver>().TakeDamage(Damage, parentTag);
            }
            Destroy(gameObject);
        }
        else if(totalDistTraveled >= MaxDistance) {
            Destroy(gameObject);
        }
        else {
            transform.Translate(Vector3.forward * forwardDistance);
            totalDistTraveled += forwardDistance;
        }
    }

    public void SetParentTag(string val) {
        parentTag = val;
    }

    public void SetDamage(float val) {
        Damage = val;
    }
}
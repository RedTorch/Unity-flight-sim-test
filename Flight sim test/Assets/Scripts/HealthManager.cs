using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public float MaxHealth = 10f;
    public GameObject ExplosionPrefab;
    private float Health;

    private bool verbose = false;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public float GetHealth() {
        return Health;
    }

    public void TakeDamage(float damage, string parentTag) {
        if(parentTag == gameObject.tag) {
            if(verbose){print("friendly fire rejected by " + gameObject.tag + " --- bullet: " + parentTag);}
            return;
        }
        if(verbose){print("damage taken by " + gameObject.tag + " --- bullet: " + parentTag);}
        Health -= damage;
        if(Health <= 0) {
            OnDestroy();
        }
    }

    void OnDestroy() {
        Instantiate(ExplosionPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
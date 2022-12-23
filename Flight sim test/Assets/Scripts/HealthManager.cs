using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public float MaxHealth = 10f;
    public GameObject ExplosionPrefab;
    public TMP_Text Data;
    private float Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Data) {
            Data.text = "Health: " + Health;
        }
    }

    public float GetHealth() {
        return Health;
    }

    public void TakeDamage(float damage, string parentTag) {
        if(parentTag == gameObject.tag) {
            print("friendly fire rejected by " + gameObject.tag + " --- bullet: " + parentTag);
            return;
        }
        print("damage taken by " + gameObject.tag + " --- bullet: " + parentTag);
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
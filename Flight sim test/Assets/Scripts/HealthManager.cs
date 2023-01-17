using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public MainUIManager uiman;
    public float MaxHealth = 10f;
    public GameObject ExplosionPrefab;
    private float Health;

    private bool verbose = false;

    public GameObject camObject;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        if(uiman != null) {
            uiman.UpdateHealthBar(Health/MaxHealth);
        }
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
        if(uiman != null) {
            uiman.UpdateHealthBar(Health/MaxHealth);
        }
        if(Health <= 0) {
            if(gameObject.tag == "Enemy") {
                GameObject.Find("MainUI").GetComponent<MainUIManager>().addKill();
            }
            Instantiate(ExplosionPrefab,transform.position,Quaternion.identity);
            if(camObject) {
                camObject.transform.parent = null;
            }
            if(uiman) {
                uiman.GameOverMenu();
            }
            Destroy(gameObject);
        }
    }
}
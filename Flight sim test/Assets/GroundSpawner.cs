using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject WorldObject;
    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject Turret;
    // Start is called before the first frame update
    void Start()
    {
        // a plane is 10mx10m; a 100 scale one is 1000mx1000m
        for(int x = -10; x < 10; x++) {
            for(int z = -10; z < 10; z++) {
                Instantiate(Ground, new Vector3(x*1000f,0f,z*1000f), Quaternion.identity, WorldObject.transform);
            }
        }
        for(int x = 0; x < 100; x++) {
            Instantiate(Turret, new Vector3(Random.Range(-2000f,2000f),0f,Random.Range(-2000f,2000f)), Quaternion.identity, WorldObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

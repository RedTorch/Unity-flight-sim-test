using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject WorldObject;
    [SerializeField] private GameObject Ground;
    public bool GroundEnabled;
    public Vector2 GroundScaleXZ; // access values via GroundScale.x, .y (even though it's actually the Z axis, not Y!)
    [SerializeField] private GameObject Turret;
    public bool TurretEnabled;
    [SerializeField] private GameObject Obstacle;
    public bool ObstacleEnabled;
    // Start is called before the first frame update
    void Start()
    {
        if(!WorldObject) {
            return;
        }
        // a plane is 10mx10m; a 100 scale one is 1000mx1000m
        if(Ground && GroundEnabled) {
            for(int x = -10; x < 10; x++) {
                for(int z = -10; z < 10; z++) {
                    Instantiate(Ground, new Vector3(x*1000f,0f,z*1000f), Quaternion.identity, WorldObject.transform);
                }
            }
        }

        if(Turret && TurretEnabled) {
            for(int x = 0; x < 100; x++) {
                Instantiate(Turret, new Vector3(Random.Range(-2000f,2000f), Random.Range(10f,500f),Random.Range(-2000f,2000f)), Quaternion.identity, WorldObject.transform);
            }
        }

        if(Obstacle && ObstacleEnabled) {
            for(int x = -10000; x < 10000; x+=1000) {
                for(int z = -10000; z < 10000; z+=1000) {
                    //create a cluster here
                    for(int obj = 0; obj < 10; obj++) {
                        Instantiate(Obstacle, new Vector3((float)x+Random.Range(-500f,500f),Random.Range(10f,50f),(float)z+Random.Range(-500f,500f)), Quaternion.identity, WorldObject.transform);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
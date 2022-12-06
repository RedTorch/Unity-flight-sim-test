using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroySelf : MonoBehaviour
{
    public float Time = 10f;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        // If there is a particle system, override the given Time and destroy when particle system is done
        ps = GetComponent<ParticleSystem>();
        if(!ps) {
            Destroy(gameObject,Time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}

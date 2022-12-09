using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroySelf : MonoBehaviour
{
    public float Time = 10f;
    private ParticleSystem ps;
    private bool isNull = false;
    // Start is called before the first frame update
    void Start()
    {
        // If there is a particle system, override the given Time and destroy when particle system is done
        if(gameObject.GetComponent<ParticleSystem>() == null) {
            Destroy(gameObject,Time);
            isNull = true;
            return;
        }
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isNull && !ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}

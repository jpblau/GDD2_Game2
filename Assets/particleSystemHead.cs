using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemHead : MonoBehaviour
{

    public List<ParticleSystem> particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGroundHit(Vector3 location)
    {
        this.gameObject.transform.position = location;

        particleSystems[0].Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemHead : MonoBehaviour
{

    public List<ParticleSystem> particleSystems;
    private bool goopEmitting;     // Is our slime on the ground and currently emitting goop?

    // Start is called before the first frame update
    void Start()
    {
        goopEmitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goopEmitting)
        {
            particleSystems[1].Pause();
        }
    }

    public void PlayGroundHit(Vector3 location)
    {
        this.gameObject.transform.position = location;

        particleSystems[0].Play();
    }

    public void PlaySlimeRoll(Vector3 location)
    {
        this.gameObject.transform.position = location;
        if (!particleSystems[1].isPlaying)
        {
            particleSystems[1].Play();
            goopEmitting = true;
        }

    }
}

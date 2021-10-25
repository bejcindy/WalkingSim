using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParticles : MonoBehaviour
{
    public ParticleSystem particles;
    public void Destroy()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}

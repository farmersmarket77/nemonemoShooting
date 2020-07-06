using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyObject", 1f);
    }

    private void DestroyObject()
    {
        ObjectPoolParticle.ReturnObject(this);
    }
}

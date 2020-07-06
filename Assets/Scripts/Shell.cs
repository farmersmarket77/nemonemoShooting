using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyObject", 3f);
    }

    private void DestroyObject()
    {
        ObjectPoolShell.ReturnObject(this);
    }
}

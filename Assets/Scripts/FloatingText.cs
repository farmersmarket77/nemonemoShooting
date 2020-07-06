using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float f_destroy_time = 0.4f;
    private Vector3 vec_offset = new Vector3(0f, 1.5f, 0f);

    private void Start()
    {
        Destroy(gameObject, f_destroy_time);

        transform.localPosition += vec_offset;
    }
}

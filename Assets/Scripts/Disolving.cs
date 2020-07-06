using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolving : MonoBehaviour
{
    public Material mat;
    public float health;
    public float maxHealth;

    private void Start()
    {
        mat.SetFloat("Vector1_79507DBC", health / maxHealth);
    }

    private void FixedUpdate()
    {
        health -= 1f;
        mat.SetFloat("Vector1_79507DBC", health / maxHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemROF : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player")
        {
            GunMaster.gunmaster.sROF(0.01f);

            Destroy(gameObject);
        }
    }
}

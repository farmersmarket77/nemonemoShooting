using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player")
        {
            GunMaster.gunmaster.sDamage(0.2f);

            Destroy(gameObject);
        }
    }
}

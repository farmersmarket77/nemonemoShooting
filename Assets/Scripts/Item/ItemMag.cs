using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMag : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player")
        {
            GunMaster.gunmaster.sMag(2);

            Destroy(gameObject);
        }
    }
}

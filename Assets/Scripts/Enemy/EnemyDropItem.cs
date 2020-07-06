using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject item_Gun;
    public GameObject item_ROF;
    public GameObject item_damage;
    
    public void DropItem()
    {
        int i_rnd = Random.Range(0, 100);

        // for test

        if (i_rnd < 33)
        {
            DropGun();
            return;
        }
        else if (i_rnd > 33 && i_rnd < 66)
        {
            DropROF();
            return;
        }
        else if (i_rnd > 66 && i_rnd < 99)
        {
            DropDamage();
            return;
        }

        //if (i_rnd < 10)
        //{
        //    DropGun();
        //}
        //else if (10 < i_rnd && i_rnd < 25)
        //{
        //    DropROF();
        //}
        //else if (25 < i_rnd && i_rnd < 50)
        //{
        //    DropDamage();
        //}
    }

    private void DropGun()
    {
        GameObject go = Instantiate(item_Gun, transform.position, Quaternion.identity);
        ItemPopMotion(go);
    }

    private void DropROF()
    {
        GameObject go = Instantiate(item_ROF, transform.position, Quaternion.identity);
        ItemPopMotion(go);
    }

    private void DropDamage()
    {
        GameObject go = Instantiate(item_damage, transform.position, Quaternion.identity);
        ItemPopMotion(go);
    }

    private void ItemPopMotion(GameObject _go)
    {
        float x, y, z;
        x = Random.Range(-1f, 2f);
        y = Random.Range(0f, 5f);
        z = Random.Range(-1f, 2f);

        _go.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z) * 2f, ForceMode.Impulse);
        _go.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 50f;
        // 10초 후 사라짐
        Destroy(_go, 10f);
    }
}

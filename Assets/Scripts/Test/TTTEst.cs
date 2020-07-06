using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTEst : MonoBehaviour
{
    private TTTEst() { }
    public static TTTEst tttest = null;

    public GameObject ob_01;
    public GameObject ob_02;
    public GameObject ob_03;

    public bool b_trigger;

    private void Awake()
    {
        if (tttest == null)
            tttest = this;
        else if (tttest != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        b_trigger = false;
    }

    private void Update()
    {
        if (b_trigger == true && Input.GetMouseButtonDown(0))
        {
            PopObject();
        }
    }

    public void PopObject()
    {
        Vector3 vec = -Vector3.one;
        vec = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, -10f, 5f));

        int rnd = Random.Range(0, 10);
        if (rnd < 3)
        {
            var go = Instantiate(ob_01, vec, Quaternion.identity);
            ItemPopMotion(go);
        }
        else if (rnd > 3 && rnd < 6)
        {
            var go = Instantiate(ob_02, vec, Quaternion.identity);
            ItemPopMotion(go);
        }
        else
        {
            var go = Instantiate(ob_03, vec, Quaternion.identity);
            ItemPopMotion(go);
        }
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

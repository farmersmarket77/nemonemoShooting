using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTouch : MonoBehaviour
{
    public GameObject img_tap;

    private int MAX_TOUCH_COUNT = 3;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject _go = Instantiate(img_tap, Input.mousePosition, Quaternion.identity);
            _go.transform.SetParent(transform.parent);
        }
    }
}

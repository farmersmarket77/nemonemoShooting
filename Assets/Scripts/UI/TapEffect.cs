using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapEffect : MonoBehaviour
{
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    private void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime);

        Color color = img.color;
        color.a = Mathf.Lerp(img.color.a, 0f, Time.deltaTime * 5);

        img.color = color;

        if (img.color.a <= 0.01f)
            Destroy(gameObject);
    }
}

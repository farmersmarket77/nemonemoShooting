using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPosition : MonoBehaviour
{
    private GunPosition() { }
    public static GunPosition gunposition = null;

    private Transform tr_player;

    private float f_speed_revolve = 100f;
    private float f_speed_rotate = 100f;


    private void Awake()
    {
        if (gunposition == null)
            gunposition = this;
        else if (gunposition != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        tr_player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Revolve();


        // 자전 중지
        //Rotate();
    }

    private void Revolve()
    {
        transform.RotateAround(tr_player.transform.position, Vector3.down, f_speed_revolve * Time.deltaTime);
        JoystickDirection joystickdirection = GameObject.Find("Joystick_Direction_BG").GetComponent<JoystickDirection>();
        transform.rotation = Quaternion.Euler(joystickdirection.vec_gun_dir);
        //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void Rotate()
    {
        transform.Rotate(Vector3.down * f_speed_rotate * Time.deltaTime);
        transform.Rotate(Vector3.left * f_speed_rotate * Time.deltaTime);
    }
}

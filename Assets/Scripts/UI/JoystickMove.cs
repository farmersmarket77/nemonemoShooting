using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private JoystickMove() { }
    public static JoystickMove instance = null;

    public RectTransform rtr_joystick_bg;
    public RectTransform rtr_joystick_stick;
    public Transform tr_player;
    public Rigidbody rigid;

    private float f_radius;
    public float f_speed = 10f;

    private Vector3 vec_move;

    private bool b_move_touch = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        f_radius = rtr_joystick_bg.rect.width * 0.5f;
    }

    private void Update()
    {
        if (PlayerManager.playermanager.b_player_dead == true)
        {
            GetComponent<JoystickMove>().enabled = false;
            return;
        }

        if (b_move_touch)
            rigid.velocity = vec_move * 50;
    }

    private void OnTouch(Vector2 _vec_touch)
    {
        Vector2 vec = new Vector2(_vec_touch.x - rtr_joystick_bg.position.x,
                                _vec_touch.y - rtr_joystick_bg.position.y);

        vec = Vector2.ClampMagnitude(vec, f_radius);
        rtr_joystick_stick.localPosition = vec;

        float f_sqr = (rtr_joystick_bg.position - rtr_joystick_stick.position).sqrMagnitude / (f_radius * f_radius);

        Vector2 vecNormal = vec.normalized;

        vec_move = new Vector3(vecNormal.x * f_speed * Time.deltaTime * f_sqr, 0f,
                            vecNormal.y * f_speed * Time.deltaTime * f_sqr);
        tr_player.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        b_move_touch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        b_move_touch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rtr_joystick_stick.localPosition = Vector2.zero;
        b_move_touch = false;
    }
}

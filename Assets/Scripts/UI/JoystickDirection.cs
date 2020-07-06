using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickDirection : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public RectTransform rtr_joystick_bg;
    public RectTransform rtr_joystick_stick;
    public Transform tr_gun;

    public Vector3 vec_gun_dir;

    private float f_radius;

    private void Start()
    {
        f_radius = rtr_joystick_bg.rect.width * 0.5f;
    }

    private void Update()
    {
        if (PlayerManager.playermanager.b_player_dead == true)
            GetComponent<JoystickDirection>().enabled = false;
    }

    private void OnTouch(Vector2 _vec_touch)
    {
        Vector2 vec = new Vector2(_vec_touch.x - rtr_joystick_bg.position.x,
                                _vec_touch.y - rtr_joystick_bg.position.y);

        vec = Vector2.ClampMagnitude(vec, f_radius);
        rtr_joystick_stick.localPosition = vec;

        Vector2 vecNormal = vec.normalized;

        tr_gun.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        vec_gun_dir = tr_gun.eulerAngles;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rtr_joystick_stick.localPosition = Vector2.zero;
    }
}

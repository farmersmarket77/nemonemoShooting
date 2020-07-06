using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CameraFollow() { }
    public static CameraFollow camerfollow = null;

    private void Awake()
    {
        if (camerfollow == null)
            camerfollow = this;
        else if (camerfollow != this)
            Destroy(gameObject);
    }

    public GameObject go_target;
    public float f_delay;

    private void LateUpdate()
    {
        if (PlayerManager.playermanager.b_player_dead == true)
            return;

        Vector3 vec_newpos = new Vector3(go_target.transform.position.x, go_target.transform.position.y + 10f,
            go_target.transform.position.z - 5.46f);

        transform.position = Vector3.Lerp(transform.position, vec_newpos, f_delay * Time.deltaTime);
    }

    public IEnumerator CameraGunFireShake(float _f_shake_time, float _f_shake_amount)
    {
        Vector3 vec_origin_pos = transform.localPosition;

        while (_f_shake_time > 0)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _f_shake_amount + vec_origin_pos;

            _f_shake_time -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = vec_origin_pos;
    }
}

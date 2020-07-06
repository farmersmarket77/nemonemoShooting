using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject go_dead_effect;
    public GameObject go_floating_text;
    public Rigidbody rigid;

    private GameObject go_player;

    private Vector3 vec_move;
    private float f_speed;
    public int i_hp;
    public bool b_hit = false;
    private bool isLanding = false;

    private void Start()
    {
        go_player = GameObject.FindGameObjectWithTag("player");
        f_speed = 2f;
        i_hp = 3;
    }

    private void Update()
    {
        if (PlayerManager.playermanager.b_player_dead == true)
        {
            GetComponent<Enemy>().enabled = false;
            return;
        }

        if (b_hit == true)
            return;

        if (Vector3.Distance(transform.position, go_player.transform.position) <= 10f && isLanding == true)
            Moving();
    }

    private void Moving()
    {
        transform.LookAt(go_player.transform);
        vec_move = (go_player.transform.position - transform.position).normalized;
        rigid.velocity = vec_move * 2f;
        //transform.Translate(Vector3.forward * f_speed * Time.deltaTime);
    }

    public void HitDeadEffect()
    {
        TakeDamage();

        if (i_hp > 0)
            return;

        GetComponent<EnemyDropItem>().DropItem();
        DeadAnim();
    }

    private void TakeDamage()
    {
        i_hp--;
        AudioMaster.audiomaster.EnemyHit();

        // trigger floating text
        if (go_floating_text)
        {
            ShowFloatingText();
        }
    }

    private void ShowFloatingText()
    {
        var go = Instantiate(go_floating_text, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = i_hp.ToString();
    }

    private void DeadAnim()
    {
        AudioMaster.audiomaster.EnemyDead();

        GameObject _go = Instantiate(go_dead_effect, transform.position, Quaternion.identity);
        Destroy(_go, 2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "player")
        {
            // player가 무적상태일시 return
            if (PlayerManager.playermanager.b_player_invincibility == true)
                return;

            PlayerManager.playermanager.Hit();
        }

        if (other.transform.tag == "floor")
        {
            isLanding = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerManager() { }
    public static PlayerManager playermanager = null;

    private void Awake()
    {
        if (playermanager == null)
            playermanager = this;
        else if (playermanager != this)
            Destroy(gameObject);
    }

    public GameObject go_dead_effect;
    public bool b_player_dead = false;
    public bool b_player_invincibility = false;

    private const float INVINCIBILITY_TIME = 0.5f;
    private int i_player_hp;


    private void Start()
    {
        InitPlayerState();
    }

    private void InitPlayerState()
    {
        i_player_hp = 3;
    }

    public void Hit()
    {
        if (i_player_hp > 1)
        {
            // player hp가 남아있을 때 피격 이벤트
            i_player_hp--;
            AudioMaster.audiomaster.Player_Hit();
            StartCoroutine(InvincibilityTime());

            return;
        }

        // player hp가 전부 소진되었을 때 사망 이벤트
        b_player_dead = true;
        AudioMaster.audiomaster.PlayerDead();
        AudioMaster.audiomaster.StopBGM();
        GameObject _go = Instantiate(go_dead_effect, transform.position, Quaternion.identity);
        Destroy(_go, 3f);
        Destroy(gameObject);
    }

    // 피격시 무적시간 부여
    private IEnumerator InvincibilityTime()
    {
        float count = 0f;
        b_player_invincibility = true;

        while (count < INVINCIBILITY_TIME)
        {
            count += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        b_player_invincibility = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Environment")
        {

        }
    }
}

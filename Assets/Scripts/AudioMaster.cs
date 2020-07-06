using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    private AudioMaster() { }
    public static AudioMaster audiomaster = null;
    private void Awake()
    {
        if (audiomaster == null)
            audiomaster = this;
        else if (audiomaster != this)
            Destroy(gameObject);
    }

    public AudioClip clip_BGM_01;

    private List<AudioClip> list_clip_enemy_hit = new List<AudioClip>();
    public AudioClip clip_enemy_spawn;
    public AudioClip clip_enemy_hit_01;
    public AudioClip clip_enemy_hit_02;
    public AudioClip clip_enemy_hit_03;
    public AudioClip clip_enemy_dead;

    private List<AudioClip> list_clip_player_hit = new List<AudioClip>();
    public AudioClip clip_player_hit_01;
    public AudioClip clip_player_hit_02;
    public AudioClip clip_player_hit_03;
    public AudioClip clip_player_hit_04;
    public AudioClip clip_player_dead;

    public AudioClip clip_gun_switch;
    public AudioClip clip_gun_switch_levor;

    public AudioClip clip_gun_glock_fire;
    public AudioClip clip_gun_glock_reloading;

    public AudioClip clip_gun_m4_fire;
    public AudioClip clip_gun_m4_reloading;

    public AudioClip clip_gun_shotgun_pump;

    private AudioSource audiosource_enemy_hit;
    private AudioSource audiosource_enemy_spawn;
    private AudioSource audiosource_enemy_dead;
    private AudioSource audiosource_player_hit;
    private AudioSource audiosource_player_dead;
    private AudioSource audiosource_gun_firing;
    private AudioSource audiosource_gun_reload;
    private AudioSource audiosource_gun_ui;


    private void Start()
    {
        InitAudioMaster();
    }

    private void InitAudioMaster()
    {
        PlayBGM();

        list_clip_enemy_hit.Add(clip_enemy_hit_01);
        list_clip_enemy_hit.Add(clip_enemy_hit_02);
        list_clip_enemy_hit.Add(clip_enemy_hit_03);
        list_clip_player_hit.Add(clip_player_hit_01);
        list_clip_player_hit.Add(clip_player_hit_02);
        list_clip_player_hit.Add(clip_player_hit_03);
        list_clip_player_hit.Add(clip_player_hit_04);

        audiosource_enemy_hit = transform.GetChild(1).GetChild(0).GetComponent<AudioSource>();
        audiosource_enemy_spawn = transform.GetChild(1).GetChild(1).GetComponent<AudioSource>();
        audiosource_enemy_dead = transform.GetChild(1).GetChild(2).GetComponent<AudioSource>();
        audiosource_player_hit = transform.GetChild(1).GetChild(3).GetComponent<AudioSource>();
        audiosource_player_dead = transform.GetChild(1).GetChild(4).GetComponent<AudioSource>();
        audiosource_gun_firing = transform.GetChild(1).GetChild(5).GetComponent<AudioSource>();
        audiosource_gun_reload = transform.GetChild(1).GetChild(6).GetComponent<AudioSource>();
        audiosource_gun_ui = transform.GetChild(1).GetChild(7).GetComponent<AudioSource>();
    }

    #region BGM

    public void PlayBGM()
    {
        transform.GetChild(0).GetComponent<AudioSource>().clip = clip_BGM_01;
        transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    public void StopBGM()
    {
        StartCoroutine(FadeoutBGM());
    }

    IEnumerator FadeoutBGM()
    {
        while (transform.GetChild(0).GetComponent<AudioSource>().volume > 0)
        {
            transform.GetChild(0).GetComponent<AudioSource>().volume -= 0.01f;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    #endregion

    #region player sound

    public void PlayerDead()
    {
        audiosource_player_dead.clip = clip_player_dead;
        audiosource_player_dead.Play();
    }

    public void Player_Hit()
    {
        int rnd = Random.Range(0, list_clip_player_hit.Count);

        audiosource_player_hit.clip = list_clip_player_hit[rnd];
        audiosource_player_hit.Play();
    }

    #endregion

    #region ui sound

    public void SwitchGun()
    {
        audiosource_gun_ui.clip = clip_gun_switch;
        audiosource_gun_ui.Play();
    }

    public void SwitchLevor()
    {
        audiosource_gun_ui.clip = clip_gun_switch_levor;
        audiosource_gun_ui.Play();
    }

    #endregion

    #region enemy sound

    public void EnemyHit()
    {
        int rnd = Random.Range(0, list_clip_enemy_hit.Count);
        
        audiosource_enemy_hit.clip = list_clip_enemy_hit[rnd];
        audiosource_enemy_hit.Play();
    }
    public void EnemySpawn()
    {
        audiosource_enemy_spawn.clip = clip_enemy_spawn;
        audiosource_enemy_spawn.Play();
    }
    public void EnemyDead()
    {
        audiosource_enemy_dead.clip = clip_enemy_dead;
        audiosource_enemy_dead.Play();
    }

    #endregion

    #region gun sounds
    
    public void Firing(AudioClip _clip)
    {
        audiosource_gun_firing.clip = _clip;
        audiosource_gun_firing.Play();
    }

    public void Reloading(AudioClip _clip)
    {
        audiosource_gun_reload.clip = _clip;
        audiosource_gun_reload.Play();
    }
    
    public void ShotgunPump()
    {
        audiosource_gun_reload.clip = clip_gun_shotgun_pump;
        audiosource_gun_reload.Play();
    }

    #region glock sound



    #endregion

    #region rifle sound

    

    #endregion

    #region shotgun sound


    #endregion

    #region sniper rifle sound


    #endregion

    #endregion
}

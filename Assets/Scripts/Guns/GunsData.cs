using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "gun")]
public class GunsData : ScriptableObject
{
    public int i_gun_number;
    public Sprite sprite_gun_image;
    public GameObject go_projectile;
    public GameObject go_Shell;
    public float f_gun_damage;
    public float f_gun_velocity;
    public float f_gun_life_time;
    public float f_gun_ROF;
    public float f_gun_reload_time;
    public int i_gun_magazine_capacity;
    public float f_gun_knockback_power;
    public float f_gun_knockback_radius;
    public float f_gun_camera_shake_time;
    public float f_gun_camera_shake_amount;
    public GameObject particle_gun_fire_effect;
    public GameObject particle_gun_impact_effect;
    public AudioClip sound_fire;
    public AudioClip sound_reload;
    public bool b_select_levor;
    public bool b_shotgun;
}

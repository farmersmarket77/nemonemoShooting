using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMaster : MonoBehaviour
{
    private GunMaster() { }
    public static GunMaster gunmaster = null;

    private void Awake()
    {
        if (gunmaster == null)
            gunmaster = this;
        else if (gunmaster != this)
            Destroy(gameObject);
    }

    // 기본 요소
    private float f_gun_master_damage;
    private Sprite sprite_gun_master_image;
    private GameObject go_gun_master_projectile;
    private GameObject go_gun_master_shell;
    private float f_gun_master_velocity;
    private float f_gun_master_life_time;
    private float f_gun_master_ROF;
    private float f_gun_master_reload_time;
    private int i_gun_master_magazie_capacity;
    private float f_gun_master_knockback_power;
    private float f_gun_master_knockback_radius;
    private float f_gun_master_camera_shake_time;
    private float f_gun_master_camera_shake_amount;
    private GameObject particle_gun_master_fire_effect;
    private GameObject particle_gun_master_impact_effect;
    private AudioClip audio_gun_fire;
    private AudioClip audio_gun_reload;
    private bool b_gun_master_select_levor;
    private bool b_gun_master_shotgun;

    public int gMagCap() { return i_gun_master_magazie_capacity; }
    public void sROF(float _f) { f_gun_master_ROF -= _f; }
    public void sDamage(float _f) { f_gun_master_damage += _f; }
    public void sMag(int _i) { i_mag += _i; }

    private const int MAX_GUN_NUMBER = 3;
    private int i_current_gun_number = 1;
    private int i_mag = 2;
    private int i_glock_cur_rounds;
    private int i_m4_cur_rounds = 0;
    private int i_shotgun_cur_rounds = 0;
    private GunsData current_gun;
    private GameObject Gun_Shape;
    public GameObject Gun_Shpe_Glock;
    public GameObject Gun_Shpe_M4;
    public GameObject Gun_Shpe_Shotgun;
    public GunsData GunsData_Glock;
    public GunsData GunsData_M4;
    public GunsData GunsData_Shotgun;
    private int i_gun_master_levor;

    public Image image_gun;
    public Button btn_start_stop_fire;
    public Button btn_reload;
    public Button btn_single;
    public Button btn_burst;
    public Button btn_auto;

    private Coroutine coroutine_fire;
    private Coroutine coroutine_reload;
    private Vector3 vec_direction;
    private bool b_can_reload = false;
    private bool b_can_fire = true;
    private int i_burst_count = 3;
    [SerializeField]
    private bool B_MASTER_FIRE = false;

    private void Start()
    {
        current_gun = GunsData_Glock;
        GrabGun();
    }

    private void FixedUpdate()
    {
        vec_direction = transform.eulerAngles;

        // 총 발사
        Fireing();

        // 장전중일시 장전버튼 비활성화
        if (b_can_reload == false)
            btn_reload.interactable = false;
        else
            btn_reload.interactable = true;

        // 탄알 최대치일시 장전버튼 비활성화
        if (i_gun_master_magazie_capacity == current_gun.i_gun_magazine_capacity)
            btn_reload.interactable = false;
        else
            btn_reload.interactable = true;
    }

    private void GrabGun()
    {
        f_gun_master_damage = current_gun.f_gun_damage;
        sprite_gun_master_image = current_gun.sprite_gun_image;
        go_gun_master_projectile = current_gun.go_projectile;
        go_gun_master_shell = current_gun.go_Shell;
        f_gun_master_velocity = current_gun.f_gun_velocity;
        f_gun_master_life_time = current_gun.f_gun_life_time;
        f_gun_master_ROF = current_gun.f_gun_ROF;
        f_gun_master_reload_time = current_gun.f_gun_reload_time;
        i_gun_master_magazie_capacity = current_gun.i_gun_magazine_capacity;
        f_gun_master_knockback_power = current_gun.f_gun_knockback_power;
        f_gun_master_knockback_radius = current_gun.f_gun_knockback_radius;
        f_gun_master_camera_shake_time = current_gun.f_gun_camera_shake_time;
        f_gun_master_camera_shake_amount = current_gun.f_gun_camera_shake_amount;
        particle_gun_master_fire_effect = current_gun.particle_gun_fire_effect;
        particle_gun_master_impact_effect = current_gun.particle_gun_impact_effect;
        audio_gun_fire = current_gun.sound_fire;
        audio_gun_reload = current_gun.sound_reload;
        b_gun_master_select_levor = current_gun.b_select_levor;
        b_gun_master_shotgun = current_gun.b_shotgun;

        i_gun_master_levor = 1;
        image_gun.sprite = sprite_gun_master_image;

        // 조정간 유무
        if (b_gun_master_select_levor == false)
        {
            btn_single.interactable = true;
            btn_burst.interactable = false;
            btn_auto.interactable = false;
        }
        else
        {
            btn_single.interactable = true;
            btn_burst.interactable = true;
            btn_auto.interactable = true;
        }
    }

    private void ChangeGun()
    {
        // 발사 중단
        B_MASTER_FIRE = false;

        switch (i_current_gun_number)
        {
            case 1:
                // Glock
                current_gun = GunsData_Glock;
                GrabGun();
                break;

            case 2:
                // M4
                current_gun = GunsData_M4;
                GrabGun();
                break;

            case 3:
                // 12gauge shotgun
                current_gun = GunsData_Shotgun;
                GrabGun();
                break;

            default:
                //Glock
                current_gun = GunsData_Glock;
                GrabGun();
                break;
        }
    }

    private void Fireing()
    {
        if (B_MASTER_FIRE == false)
            return;

        if (i_gun_master_magazie_capacity < 1 || b_can_fire == false)
        {
            // 나중에 구현? 3 ~ 5발이내로 탄창 빌경우 추가음성도 추가할지도..?
            //AudioMaster.audiomaster.DryFire();
            return;
        }

        // 재장전 가능
        b_can_reload = true;

        // 탄알생성
        if (i_current_gun_number == 3)
        {
            // 샷건
            for (int i = 0; i < 5; i++)
            {
                float f_rnd_y = Random.Range(vec_direction.y - 5f, vec_direction.y + 5f);
                vec_direction = new Vector3(vec_direction.x, f_rnd_y, vec_direction.z);
                //GameObject _go = Instantiate(current_gun.go_projectile, transform.position, Quaternion.identity);
                var _go = ObjectPoolProjectile.GetObject(current_gun.go_projectile);
                // 탄알에 속성부여
                _go.GetComponent<ProjectileMaster>().GetProjectileProperties(vec_direction, f_gun_master_damage,
                    f_gun_master_velocity, f_gun_master_life_time, f_gun_master_knockback_power,
                    f_gun_master_knockback_radius, particle_gun_master_impact_effect);
            }
        }
        else
        {
            // 그 외 총기류
            // GameObject _go = Instantiate(current_gun.go_projectile, transform.position, Quaternion.identity);
            var _go = ObjectPoolProjectile.GetObject(current_gun.go_projectile);
            // 탄알에 속성부여
            _go.GetComponent<ProjectileMaster>().GetProjectileProperties(vec_direction, f_gun_master_damage,
                f_gun_master_velocity, f_gun_master_life_time, f_gun_master_knockback_power,
                f_gun_master_knockback_radius, particle_gun_master_impact_effect);
        }
        // 발사음재생
        AudioMaster.audiomaster.Firing(audio_gun_fire);
        // 카메라쉐이킹
        //StartCoroutine(CameraFollow.camerfollow.CameraGunFireShake(f_gun_master_camera_shake_time, f_gun_master_camera_shake_amount));
        // 발사이펙트
        FireEffect();
        // 탄피배출
        ShellEject();

        // 발사불가능 -> 코루틴 -> FixLevor안에서 조작
        b_can_fire = false;
        i_gun_master_magazie_capacity--;

        // 조정간조작 -> 가장 마지막에 있어야함
        FixLevor();
    }

    private void FireEffect()
    {
        GameObject _go = Instantiate(particle_gun_master_fire_effect, transform.position, transform.rotation);
        Destroy(_go, 1f);
    }

    #region reloading

    private IEnumerator Reloading()
    {
        B_MASTER_FIRE = false;
        btn_reload.interactable = false;
        float f_tmp_reload_time = f_gun_master_reload_time;
        b_can_reload = false;
        b_can_fire = false;
        i_burst_count = 3;
        StopCoroutine(coroutine_fire);
        AudioMaster.audiomaster.Reloading(audio_gun_reload);

        while (f_tmp_reload_time > 0)
        {
            f_tmp_reload_time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        i_gun_master_magazie_capacity = current_gun.i_gun_magazine_capacity;
        b_can_reload = false;
        b_can_fire = true;
        btn_reload.interactable = true;
    }

    private IEnumerator ReloadingShotgun()
    {

        b_can_reload = false;
        b_can_fire = false;
        i_burst_count = 3;
        
        while (i_gun_master_magazie_capacity < current_gun.i_gun_magazine_capacity)
        {
            btn_reload.interactable = false;
            if (B_MASTER_FIRE == true)
                break;

            AudioMaster.audiomaster.Reloading(audio_gun_reload);
            i_gun_master_magazie_capacity++;
            b_can_reload = false;
            b_can_fire = true;
            yield return new WaitForSeconds(f_gun_master_reload_time);
        }

        //B_MASTER_FIRE = false;
        btn_reload.interactable = true;
    }

    #endregion


    #region shelleject

    private void ShellEject()
    {
        // 샷건 탄피배출
        if (i_current_gun_number == 3)
        {
            StartCoroutine(ShotgunShellEject());
            return;
        }
        //GameObject _go = Instantiate(go_gun_master_shell, transform.position, Quaternion.identity);
        var _go = ObjectPoolShell.GetObject(go_gun_master_shell);

        float x, y, z;
        x = Random.Range(-1f, 2f);
        y = Random.Range(0f, 3f);
        z = Random.Range(-1f, 2f);

        _go.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z) * 2f, ForceMode.Impulse);
        _go.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 50f;
        
    }

    private IEnumerator ShotgunShellEject()
    {
        float f_time = f_gun_master_ROF / 2;

        while (f_time > 0)
        {
            f_time -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        AudioMaster.audiomaster.ShotgunPump();
        //GameObject _go = Instantiate(go_gun_master_shell, transform.position, Quaternion.identity);
        var _go = ObjectPoolShell.GetObject(go_gun_master_shell);

        float x, y, z;
        x = Random.Range(-1f, 2f);
        y = Random.Range(0f, 3f);
        z = Random.Range(-1f, 2f);

        _go.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z) * 2f, ForceMode.Impulse);
        _go.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 50f;
        
    }

    #endregion

    #region levor

    private void FixLevor()
    {
        // 조정간 조작
        if (i_gun_master_levor == 1)
            coroutine_fire = StartCoroutine(FireRateSingle());
        if (i_gun_master_levor == 2)
            coroutine_fire = StartCoroutine(FireRateBurst());
        if (i_gun_master_levor == 3)
            coroutine_fire = StartCoroutine(FireRateFullAuto());
    }

    private IEnumerator FireRateSingle()
    {
        float f_tmp_ROF = f_gun_master_ROF;
        
        while (f_tmp_ROF > 0)
        {
            f_tmp_ROF -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        b_can_fire = true;
    }

    private IEnumerator FireRateBurst()
    {
        float f_tmp_ROF = f_gun_master_ROF / 12f;
        i_burst_count--;

        while (f_tmp_ROF > 0)
        {
            f_tmp_ROF -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (i_burst_count <= 0)
        {
            i_burst_count = 3;
            yield return new WaitForSeconds(f_gun_master_ROF * 1.2f);
        }

        b_can_fire = true;
    }

    private IEnumerator FireRateFullAuto()
    {
        float f_tmp_ROF = f_gun_master_ROF / 12f;

        while (f_tmp_ROF > 0)
        {
            f_tmp_ROF -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        b_can_fire = true;
    }

    #endregion

    #region ui buttons

    public void ButtonReloading()
    {
        // Glock 재장전
        if (i_current_gun_number == 1)
            StartCoroutine(Reloading());

        // M4 재장전
        if (i_current_gun_number == 2 && i_mag > 0)
            StartCoroutine(Reloading());

        // 샷건 재장전
        if (i_current_gun_number == 3 && i_mag > 0)
        {
            B_MASTER_FIRE = false;
            coroutine_reload = StartCoroutine(ReloadingShotgun());
        }
    }

    public void ButtonChangeGunRight()
    {
        i_current_gun_number++;

        if (i_current_gun_number > MAX_GUN_NUMBER)
        {
            i_current_gun_number = 1;
        }

        ChangeGun();
        AudioMaster.audiomaster.SwitchGun();
    }

    public void ButtonChangeGunLeft()
    {
        i_current_gun_number--;

        if (i_current_gun_number < 1)
        {
            i_current_gun_number = MAX_GUN_NUMBER;
        }

        ChangeGun();
        AudioMaster.audiomaster.SwitchGun();
    }

    public void ButtonFireTypeSingle()
    {
        i_gun_master_levor = 1;
        AudioMaster.audiomaster.SwitchLevor();
    }

    public void ButtonFireTypeBurst()
    {
        i_gun_master_levor = 2;
        AudioMaster.audiomaster.SwitchLevor();
    }

    public void ButtonFireTypeFullAuto()
    {
        i_gun_master_levor = 3;
        AudioMaster.audiomaster.SwitchLevor();
    }

    public void ButtonStartStopFire()
    {
        B_MASTER_FIRE = !B_MASTER_FIRE;
    }

    #endregion
}

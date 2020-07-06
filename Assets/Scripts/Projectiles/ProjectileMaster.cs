using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMaster : MonoBehaviour
{    
    // 기본 요소
    private float f_projectile_damage;
    private float f_projectile_velocity;
    private float f_projectile_life_time;
    private float f_knockback_power;
    private float f_knockback_radius;
    private GameObject particle_master_impact_effect;

    private Vector3 vec_direction;

    private void Update()
    {
        Flight();
    }

    private void Flight()
    {
        transform.rotation = Quaternion.Euler(vec_direction);
        GetComponent<Rigidbody>().AddForce(transform.forward * f_projectile_velocity);
        f_projectile_life_time -= Time.deltaTime;
        
        Invoke("DestroyProjectile", 2f);

        //if (f_projectile_life_time < 0)
        //    Destroy(gameObject);
    }

    public void GetProjectileProperties(Vector3 _vec, float _f_damage, float _f_velocity,
        float _f_life_time,float _f_knockback_power, float _f_knockback_radius, GameObject _particle)
    {
        vec_direction = _vec;
        f_projectile_damage = _f_damage;
        f_projectile_velocity = _f_velocity;
        f_projectile_life_time = _f_life_time;
        particle_master_impact_effect = _particle;
        f_knockback_power = _f_knockback_power;
        f_knockback_radius = _f_knockback_radius;
    }
    
    private void HitTarget(GameObject _go)
    {
        Vector3 vec_pos = transform.position - _go.transform.position;
        GameObject tmp_go = Instantiate(particle_master_impact_effect, transform.position, Quaternion.LookRotation(vec_pos));
        //tmp_go.transform.localRotation = Quaternion.Euler((transform.position - _go.transform.position).normalized);
        //Debug.Log(tmp_go.transform.localRotation);

        ObjectPoolProjectile.ReturnObject(this);
        //Destroy(tmp_go, 2f);
    }

    private void Explosion(GameObject _go)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, f_knockback_radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigid = colliders[i].GetComponent<Rigidbody>();

            if (rigid != null)
            {
                rigid.AddExplosionForce(f_knockback_power * 10f, transform.position, f_knockback_radius);
                _go.GetComponent<Enemy>().b_hit = true;
            }
        }
    }

    private void Knockback(GameObject _go)
    {
        float f_time = 0.2f;

        while (f_time > 0f)
        {
            _go.GetComponent<Rigidbody>().AddForce((_go.transform.position - transform.position).normalized * f_knockback_power);
            _go.GetComponent<Enemy>().b_hit = true;
            f_time -= Time.deltaTime;
        }
    }

    private void DestroyProjectile()
    {
        ObjectPoolProjectile.ReturnObject(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            HitTarget(other.gameObject);
            Knockback(other.gameObject);
            other.GetComponent<Enemy>().b_hit = false;
            other.GetComponent<Enemy>().HitDeadEffect();
            //ObjectPoolProjectile.ReturnObject(this);
        }
    }
}

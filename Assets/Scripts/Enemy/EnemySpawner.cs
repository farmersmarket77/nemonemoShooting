using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject go_enemy;

    private GameObject target;
    private Vector3 vec_origin_scale;
    private Coroutine coroutine_spawn;
    private const int I_MAX_ENEMY_COUNT = 10;
    private float f_active_time;
    private float f_spawn_time;
    private int i_current_enemy_count;
    private bool b_activation = false;

    private void Start()
    {
        f_spawn_time = 0.5f;
        f_active_time = 2f;
        target = GameObject.FindGameObjectWithTag("player");
        transform.GetComponent<Disolving>().enabled = false;

        //coroutine = StartCoroutine(SpawnEnemy());
        //StartCoroutine(ActiveSpawner());
    }

    private void Update()
    {
        if (PlayerManager.playermanager.b_player_dead == true)
        {
            GetComponent<EnemySpawner>().enabled = false;
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= 10f &&
            b_activation == false)
        {
            transform.GetComponent<Disolving>().enabled = true;
            StartCoroutine(ActiveSpawner());
            coroutine_spawn = StartCoroutine(SpawnEnemy());
        }

        if (PlayerManager.playermanager.b_player_dead == true)
        {
            GetComponent<EnemySpawner>().enabled = false;
            StopCoroutine(coroutine_spawn);
        }
    }

    private IEnumerator ActiveSpawner()
    {
        b_activation = true;
        Vector3 tmp_vec_origin = transform.localScale;
        float tmp_f_active_time = f_active_time;
        //transform.localScale = Vector3.zero;

        while (f_active_time > 0)
        {
            //transform.localScale += new Vector3(tmp_vec_origin.x / tmp_f_active_time * Time.deltaTime,
            //                                    tmp_vec_origin.y / tmp_f_active_time * Time.deltaTime,
            //                                    tmp_vec_origin.z / tmp_f_active_time * Time.deltaTime);
            f_active_time -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        f_active_time = 2f;
        //coroutine_spawn = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (i_current_enemy_count < I_MAX_ENEMY_COUNT)
        {
            var enemy = Instantiate(go_enemy, transform.position, Quaternion.identity);
            enemy.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
            AudioMaster.audiomaster.EnemySpawn();

            i_current_enemy_count++;

            yield return new WaitForSeconds(f_spawn_time);
        }

        StartCoroutine(DeleteSpawner());
    }


    private IEnumerator DeleteSpawner()
    {
        while (f_active_time > 0)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            f_active_time -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(gameObject);
    }
}

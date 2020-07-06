using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolParticle : MonoBehaviour
{
    private ObjectPoolParticle() { }
    public static ObjectPoolParticle instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // 풀에 저장해놓을 오브젝트 갯수 생성
        Initialize(10, enemyDeadParticleQueue, enemyDead);
        Initialize(10, playerDeadParticleQueue, playerDead);
        Initialize(10, glockFireParticleQueue, glockFire);
        Initialize(10, glockImpactParticleQueue, glockImpact);
        Initialize(10, M4FireParticleQueue, M4Fire);
        Initialize(10, M4ImpactParticleQueue, M4Impact);
        Initialize(10, shotgunFireParticleQueue, shotgunFire);
        Initialize(10, shotgunImpactParticleQueue, shotgunImpact);
    }

    public GameObject enemyDead;
    public GameObject playerDead;
    public GameObject glockFire;
    public GameObject glockImpact;
    public GameObject M4Fire;
    public GameObject M4Impact;
    public GameObject shotgunFire;
    public GameObject shotgunImpact;
    private Queue<ParticlePool> enemyDeadParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> playerDeadParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> glockFireParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> glockImpactParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> M4FireParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> M4ImpactParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> shotgunFireParticleQueue = new Queue<ParticlePool>();
    private Queue<ParticlePool> shotgunImpactParticleQueue = new Queue<ParticlePool>();

    private void Initialize(int _initCount, Queue<ParticlePool> _particleQueue, GameObject _particle)
    {
        for (int i = 0; i < _initCount; i++)
        {
            _particleQueue.Enqueue(CreateNew(_particle));
        }
    }

    // 오브젝트 생성
    private ParticlePool CreateNew(GameObject _particle)
    {
        var newObj = Instantiate(_particle).GetComponent<ParticlePool>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static ParticlePool GetObject(GameObject _particle)
    {
        Queue<ParticlePool> _particleQueue = null;

        if (_particle == instance.enemyDead)
            _particleQueue = instance.enemyDeadParticleQueue;
        else if (_particle == instance.playerDead)
            _particleQueue = instance.playerDeadParticleQueue;
        else if (_particle == instance.glockFire)
            _particleQueue = instance.glockFireParticleQueue;
        else if (_particle == instance.glockImpact)
            _particleQueue = instance.glockImpactParticleQueue;
        else if (_particle == instance.M4Fire)
            _particleQueue = instance.M4FireParticleQueue;
        else if (_particle == instance.M4Impact)
            _particleQueue = instance.M4ImpactParticleQueue;
        else if (_particle == instance.shotgunFire)
            _particleQueue = instance.shotgunFireParticleQueue;
        else if (_particle == instance.shotgunImpact)
            _particleQueue = instance.shotgunImpactParticleQueue;

        // 풀에서 꺼내감
        if (_particleQueue.Count > 0)
        {
            var obj = _particleQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        // 풀에 여분이 없을때 새로운 오브젝트 생성
        else
        {
            var newObj = instance.CreateNew(_particle);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    // 풀에 반납
    public static void ReturnObject(ParticlePool _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.SetParent(instance.transform);
        instance.SelectQueue(_obj);
    }

    public void SelectQueue(ParticlePool _obj)
    {
        if (_obj == instance.enemyDead)
            enemyDeadParticleQueue.Enqueue(_obj);
        else if (_obj == instance.playerDead)
            playerDeadParticleQueue.Enqueue(_obj);
        else if (_obj == instance.glockFire)
            glockFireParticleQueue.Enqueue(_obj);
        else if (_obj == instance.glockImpact)
            glockImpactParticleQueue.Enqueue(_obj);
        else if (_obj == instance.M4Fire)
            M4FireParticleQueue.Enqueue(_obj);
        else if (_obj == instance.M4Impact)
            M4ImpactParticleQueue.Enqueue(_obj);
        else if (_obj == instance.shotgunFire)
            shotgunFireParticleQueue.Enqueue(_obj);
        else if (_obj == instance.shotgunImpact)
            shotgunImpactParticleQueue.Enqueue(_obj);
    }
}

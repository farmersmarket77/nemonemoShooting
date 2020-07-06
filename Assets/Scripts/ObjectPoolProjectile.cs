using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolProjectile : MonoBehaviour
{
    private ObjectPoolProjectile() { }
    public static ObjectPoolProjectile instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // 풀에 저장해놓을 오브젝트 갯수 생성
        Initialize(20, glockProjectileQueue, glockProjectile);
        Initialize(20, M4ProjectileQueue, M4Projectile);
        Initialize(20, shotgunProjectileQueue, shotgunProjectile);
    }

    public GameObject glockProjectile;
    public GameObject M4Projectile;
    public GameObject shotgunProjectile;
    private Queue<ProjectileMaster> glockProjectileQueue = new Queue<ProjectileMaster>();
    private Queue<ProjectileMaster> M4ProjectileQueue = new Queue<ProjectileMaster>();
    private Queue<ProjectileMaster> shotgunProjectileQueue = new Queue<ProjectileMaster>();

    private void Initialize(int _initCount, Queue<ProjectileMaster> _projectileQueue, GameObject _projectile)
    {
        for (int i = 0; i < _initCount; i++)
        {
            _projectileQueue.Enqueue(CreateNew(_projectile));
        }
    }

    // 오브젝트 생성
    private ProjectileMaster CreateNew(GameObject _projectile)
    {
        var newObj = Instantiate(_projectile).GetComponent<ProjectileMaster>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static ProjectileMaster GetObject(GameObject _projectile)
    {
        Queue<ProjectileMaster> _projectileQueue = null;

        if (_projectile == instance.glockProjectile)
            _projectileQueue = instance.glockProjectileQueue;
        else if (_projectile == instance.M4Projectile)
            _projectileQueue = instance.M4ProjectileQueue;
        else if (_projectile == instance.shotgunProjectile)
            _projectileQueue = instance.shotgunProjectileQueue;

        // 풀에서 꺼내감
        if (_projectileQueue.Count > 0)
        {
            var obj = _projectileQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        // 풀에 여분이 없을때 새로운 오브젝트 생성
        else
        {
            var newObj = instance.CreateNew(_projectile);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    // 풀에 반납
    public static void ReturnObject(ProjectileMaster _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.SetParent(instance.transform);
        instance.SelectQueue(_obj);
    }

    public void SelectQueue(ProjectileMaster _obj)
    {
        if (_obj == glockProjectile)
            glockProjectileQueue.Enqueue(_obj);
        else if (_obj == M4Projectile)
            M4ProjectileQueue.Enqueue(_obj);
        else if (_obj == shotgunProjectile)
            shotgunProjectileQueue.Enqueue(_obj);
                
    }
}

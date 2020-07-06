using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolShell : MonoBehaviour
{
    private ObjectPoolShell() { }
    public static ObjectPoolShell instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // 풀에 저장해놓을 오브젝트 갯수 생성
        Initialize(20, glockShellQueue, glockShell);
        Initialize(20, M4ShellQueue, M4Shell);
        Initialize(20, shotgunShellQueue, shotgunShell);
    }

    public GameObject glockShell;
    public GameObject M4Shell;
    public GameObject shotgunShell;
    private Queue<Shell> glockShellQueue = new Queue<Shell>();
    private Queue<Shell> M4ShellQueue = new Queue<Shell>();
    private Queue<Shell> shotgunShellQueue = new Queue<Shell>();

    private void Initialize(int _initCount, Queue<Shell> _shellQueue, GameObject _shell)
    {
        for (int i = 0; i < _initCount; i++)
        {
            _shellQueue.Enqueue(CreateNew(_shell));
        }
    }

    // 오브젝트 생성
    private Shell CreateNew(GameObject _shell)
    {
        var newObj = Instantiate(_shell).GetComponent<Shell>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Shell GetObject(GameObject _shell)
    {
        Queue<Shell> _projectileQueue = null;

        if (_shell == instance.glockShell)
            _projectileQueue = instance.glockShellQueue;
        else if (_shell == instance.M4Shell)
            _projectileQueue = instance.M4ShellQueue;
        else if (_shell == instance.shotgunShell)
            _projectileQueue = instance.shotgunShellQueue;

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
            var newObj = instance.CreateNew(_shell);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    // 풀에 반납
    public static void ReturnObject(Shell _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.SetParent(instance.transform);
        instance.SelectQueue(_obj);
    }

    public void SelectQueue(Shell _obj)
    {
        if (_obj == glockShell)
            glockShellQueue.Enqueue(_obj);
        else if (_obj == M4Shell)
            M4ShellQueue.Enqueue(_obj);
        else if (_obj == shotgunShell)
            shotgunShellQueue.Enqueue(_obj);

    }
}

using Unity.VisualScripting;
using UnityEngine;
public abstract class StaticInstance<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T instance;
    protected virtual void Awake() => instance = this as T;
    protected internal virtual void OnApplicationQuit()
    {
        instance = null;
        Destroy(gameObject);
    }
    public T GetInstance()
    {
        if (instance is null)
        {
            GameObject newInstance = new GameObject();
            newInstance.AddComponent<T>();
        }
        return instance;
    }
}



public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (instance is not null)
        {
            Destroy(gameObject);
        }
        base.Awake();
    }
}

public abstract class SingletonPersistent<T> : Singleton<T> where T: MonoBehaviour
{
    protected override void Awake()
    {
        //Destroy this if it already exists
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
} 
using UnityEngine;
public abstract class StaticInstance<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T instance {get; private set;}
    protected virtual void Awake() => instance = this as T;
    protected internal virtual void OnApplicationQuit()
    {
        instance = null;
        Destroy(gameObject);
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
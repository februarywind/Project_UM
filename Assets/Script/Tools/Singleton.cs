using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Init();
            }
            return instance;
        }
    }
    private static void Init()
    {
        instance = FindAnyObjectByType<T>();
        if (instance == null)
        {
            GameObject gameObject = new(typeof(T).Name);
            instance = gameObject.AddComponent<T>();
        }
        DontDestroyOnLoad(instance.gameObject);
    }
}

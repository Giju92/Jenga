using UnityEngine;

/// <summary>
/// Abstract class to implement the Singleton pattern
/// It is mandatory to ensure the presence of the defined class in the scene 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseSingleton<T> : MonoBehaviour, ISingleton<T> where T : MonoBehaviour
{
    private static T _instance = null;

    // Only for test
    protected static void SetInstance(T reference)
    {
        _instance = reference;
    }

    public static T GetInstance()
    {
        if (_instance == null)
            _instance = (T)FindObjectOfType(typeof(T));

        return _instance;
    }

    T ISingleton<T>.GetInstance()
    {
        return GetInstance();
    }

    void OnApplicationQuit()
    {
        // release reference on exit
        _instance = null;
    }
}
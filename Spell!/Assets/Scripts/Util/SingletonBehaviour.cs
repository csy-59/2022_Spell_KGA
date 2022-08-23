using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject Object = new GameObject();
                    instance = Object.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            if(instance.gameObject != this)
            {
                Destroy(gameObject);
            }
        }

        instance = GetComponent<T>();
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

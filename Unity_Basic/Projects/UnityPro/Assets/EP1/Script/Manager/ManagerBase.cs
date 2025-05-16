using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBehaviour
{
    
    protected void DontDestroy<T>() where T : Object
    {
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}

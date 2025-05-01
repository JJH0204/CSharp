using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : ManagerBase
{
    // Singleton
    // 오브젝트가 씬에 하나만 존재하도록 보장하는 패턴
    private static SystemManager instance = null;

    public static SystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SystemManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SystemManager");
                    instance = obj.AddComponent<SystemManager>();
                }
            }
            return instance;
        }
    }

    public bool IsInit { get; set; } = false;

    private void Awake()
    {
        DontDestroy<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }
}

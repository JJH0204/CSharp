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
    public string ApiUrl { get; set; } = string.Empty;

    public string DevelopmentID { get
        {
            return PlayerPrefs.GetString("DevelopmentID");
        } set
        {
            PlayerPrefs.SetString("DevelopmentID", value);
        }
    }

    public DEVELOPER_ID_AUTHORITY dEVELOPER_ID_AUTHORITY { get; set; } = DEVELOPER_ID_AUTHORITY.NONE;

    private void Awake()
    {
        DontDestroy<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : ManagerBase
{
    #region Singleton
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
    #endregion

    #region Variables
    public bool IsInit { get; set; } = false;
    public string ApiUrl { get; set; } = string.Empty;
    public DEVELOPER_ID_AUTHORITY developerIdAuthority { get; set; } = DEVELOPER_ID_AUTHORITY.NONE;
    public string DevelopmentID
    {
        get
        {
            return PlayerPrefs.GetString("DevelopmentID");
        }
        set
        {
            PlayerPrefs.SetString("DevelopmentID", value);
        }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        DontDestroy<SystemManager>();
    }
    #endregion

    #region Methods
    public void SetInit()
    {
        IsInit = true;
    }
    #endregion
}

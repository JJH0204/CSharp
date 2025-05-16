using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStoregeManager : ManagerBase
{
    #region Singleton
    private static LocalStoregeManager instance = null;

    public static LocalStoregeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalStoregeManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("LocalStoregeManager");
                    instance = obj.AddComponent<LocalStoregeManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Variables
    private bool isInit = false;
    #endregion

    #region Methods
    public void SetInit()
    {
        isInit = true;
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<LocalStoregeManager>();
    }
    #endregion
}

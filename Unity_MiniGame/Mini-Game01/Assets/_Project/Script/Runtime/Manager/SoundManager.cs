using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ManagerBase
{
    #region Singleton
    
    private static SoundManager _instance;

    public static SoundManager instance
    {
        get
        {
            if (_instance is not null) return _instance;
            _instance = FindObjectOfType<SoundManager>();
            if (_instance is not null) return _instance;
            var obj = new GameObject("SoundManager");
            _instance = obj.AddComponent<SoundManager>();
            return _instance;
        }
    }

    #endregion
    
    #region Unity Methods
    
    private void Awake()
    {
        DontDestroy<SoundManager>();
    }

    private void Start()
    {
        // TitleSceneInit.GetBgmPrefab();
    }
    
    #endregion
    
    
}

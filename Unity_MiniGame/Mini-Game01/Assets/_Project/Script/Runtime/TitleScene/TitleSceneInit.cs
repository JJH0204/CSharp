using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TitleSceneInit : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private GameObject bgmPrefab;
    
    private GameObject _bgmInstance;

    private bool _isInit;
    
    #endregion
    
    #region Unity Methods
    private void Awake()
    {
        _isInit = CheckInit();
    }
    
    private void Start()
    {
        if (!_isInit) return;
        _bgmInstance = Instantiate(bgmPrefab);
    }
    
    #endregion

    #region Methods
    private static bool CheckInit()
    {
        var managers = FindObjectsByType<ManagerBase>(FindObjectsSortMode.None);
        if (managers.Length >= 1) return true;
        // 생성되된 매니저가 없다면 Init씬으로 이동
        SceneLoadManager.instance.LoadScene(SceneType.Init);
        return false;
    }
    #endregion
}

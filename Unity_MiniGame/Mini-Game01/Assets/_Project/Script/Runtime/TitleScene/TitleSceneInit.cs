using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneInit : MonoBehaviour
{
    #region Variables
    #endregion

    #region Cache
    // private TitleSceneUI titleSceneUI;
    #endregion

    #region Unity Methods
    void Awake()
    {
        CheckInit();
    }
    #endregion

    #region Methods
    private static void CheckInit()
    {
        ManagerBase[] managers = FindObjectsByType<ManagerBase>(FindObjectsSortMode.None);
        if (managers.Length < 1)
        {
            // 생성되된 매니저가 없다면 Init씬으로 이동
            SceneLoadManager.Instance.LoadScene(SCENE_TYPE.INIT);
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInit : MonoBehaviour
{
    #region Variables

    #endregion

    #region Cache
    // private TitleSceneUI titleSceneUI;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (CheckInit())
        {
            // Game
        }
    }
    #endregion

    #region Methods
    private static bool CheckInit()
    {
        ManagerBase[] managers = FindObjectsByType<ManagerBase>(FindObjectsSortMode.None);
        if (managers.Length < 1)
        {
            // 생성되된 매니저가 없다면 Init씬으로 이동
            SceneLoadManager.Instance.LoadScene(SCENE_TYPE.INIT);
            return false;
        }
        return true;
    }
    #endregion
}

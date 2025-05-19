using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    #region 공유 메서드
    protected void DontDestroy<T>() where T : Object
    {
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region 추상화 메서드
    public abstract void Init(); // 메니저 초기화 메서드
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<ManagerBase> managers = null;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        managers = new List<ManagerBase>
        {
            // 메니저 인스턴스 생성
            GameManager.Instance,
            InputManager.Instance,
            NoteSystemManager.Instance
        };

        // 메니저 초기화
        foreach (ManagerBase manager in managers)
        {
            manager.Init();
        }
    }
    #endregion
}

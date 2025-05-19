using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSystemManager : ManagerBase
{
    #region Singleton
    private static NoteSystemManager instance = null;

    public static NoteSystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NoteSystemManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("NoteSystemManager");
                    instance = obj.AddComponent<NoteSystemManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Variables
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<NoteSystemManager>();
    }
    #endregion

    #region Override Methods
    public override void Init()
    {
        // NoteSystemManager 초기화
        // 예: 노트 시스템 설정, 이벤트 리스너 등록 등
    }
    #endregion

    #region Custom Methods
    // 노트 시스템 관련 메서드들
    public void OnInput_Func(KeyCode keyCode)
    {
        // 노트 입력 처리
        Debug.Log("노트 입력: " + keyCode);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : ManagerBase
{
    #region Singleton
    private static InputManager instance = null;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("InputManager");
                    instance = obj.AddComponent<InputManager>();
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
        DontDestroy<InputManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            NoteSystemManager.Instance.OnInput_Func(KeyCode.A);
        }

        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            NoteSystemManager.Instance.OnInput_Func(KeyCode.S);
        }

        if (Input.GetKeyDown(KeyCode.D) == true)
        {
            NoteSystemManager.Instance.OnInput_Func(KeyCode.D);
        }
    }
    #endregion

    #region Override Methods
    public override void Init()
    {
        // InputManager 초기화
        // 예: 키 입력 설정, 이벤트 리스너 등록 등
    }
    #endregion
}

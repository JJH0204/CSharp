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
    public bool IsInit { get; private set; } = false;
    #endregion

    #region Cache
    private GameSceneUI gameSceneUI = null;
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<InputManager>();
    }

    void Start()
    {
        if (gameSceneUI == null)
        {
            gameSceneUI = FindAnyObjectByType<GameSceneUI>();
            if (gameSceneUI == null)
            {
                Debug.LogError("GameSceneUI not found in the scene.");
            }
        }
    }

    void Update()
    {
        // 버리기 단축기 (A)
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            // NoteSystemManager.Instance.OnInput_Func(KeyCode.A);
            gameSceneUI.OnClick_ThrowButton();
        }

        // 담기 단축기 (S)
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            // NoteSystemManager.Instance.OnInput_Func(KeyCode.S);
            gameSceneUI.OnClick_CatchButton();
        }

        // if (Input.GetKeyDown(KeyCode.Space) == true)
        // {
        //     NoteSystemManager.Instance.OnInput_Func(KeyCode.D);
        // }
    }
    #endregion

    #region Override Methods
    public override bool Init()
    {
        // InputManager 초기화
        
        return IsInit;
    }
    #endregion
}

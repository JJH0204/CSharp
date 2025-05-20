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
    // public override bool IsInit { get; private set; } = false;
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
        
    }

    void Update()
    {
        // Init(); // 초기화 체크
        if (!(this.IsInit = Init()))
            return;

        // 버리기 단축기 (A)
        if (Input.GetKeyDown(KeyCode.A))
        {
            // NoteSystemManager.Instance.OnInput_Func(KeyCode.A);
            gameSceneUI.OnClick_ThrowButton();
        }

        // 담기 단축기 (S)
        if (Input.GetKeyDown(KeyCode.S))
        {
            // NoteSystemManager.Instance.OnInput_Func(KeyCode.S);
            gameSceneUI.OnClick_CatchButton();
        }
    }

    // private void KeyInputCheck()
    // {
    //     if (SceneLoadManager.Instance.CurrentSceneType == SCENE_TYPE.GAME)
    //     {
            // 버리기 단축기 (A)
//         if (Input.GetKeyDown(KeyCode.A))
//         {
//             // NoteSystemManager.Instance.OnInput_Func(KeyCode.A);
//             gameSceneUI.OnClick_ThrowButton();
//         }
// // 담기 단축기 (S)
// if (Input.GetKeyDown(KeyCode.S))
// {
//     // NoteSystemManager.Instance.OnInput_Func(KeyCode.S);
//     gameSceneUI.OnClick_CatchButton();
// }
//     }
// }
#endregion

#region Override Methods
public override bool Init()
    {
        // InputManager 초기화
        if (SceneLoadManager.Instance.CurrentSceneType == SCENE_TYPE.GAME)
        {
            // 게임 씬에서만 초기화
            gameSceneUI = FindObjectOfType<GameSceneUI>();
            if (gameSceneUI == null)
            {
                // Debug.LogError("GameSceneUI not found in the scene.");
                return false;
            }
            
        }
        return true;
    }
    #endregion
}

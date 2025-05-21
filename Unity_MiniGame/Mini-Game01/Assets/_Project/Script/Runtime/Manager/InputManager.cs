using UnityEngine;

public class InputManager : ManagerBase
{
    #region Singleton
    private static InputManager _instance;

    public static InputManager instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<InputManager>();
                if (_instance is null)
                {
                    GameObject obj = new GameObject("InputManager");
                    _instance = obj.AddComponent<InputManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Cache
    
    private GameSceneUI _gameSceneUI;
    
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<InputManager>();
    }

    void Update()
    { 
        if (SceneLoadManager.instance.IsSceneGame() && GameManager.instance.IsGamePlaying())
        {
            if (_gameSceneUI is null)
                _gameSceneUI = FindAnyObjectByType<GameSceneUI>();
            
            // 버리기 단축기 (A)
            if (Input.GetKeyDown(KeyCode.A))
                _gameSceneUI.OnClick_ThrowButton();

            // 담기 단축기 (S)
            if (Input.GetKeyDown(KeyCode.S))
                _gameSceneUI.OnClick_CatchButton();
        }
        
    }
#endregion
}

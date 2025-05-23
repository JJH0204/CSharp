using UnityEngine;

public class GameSceneInit : MonoBehaviour
{
    #region Singleton
    private static GameSceneInit _instance;

    public static GameSceneInit instance
    {
        get
        {
            if (_instance is not null) return _instance;
            _instance = FindObjectOfType<GameSceneInit>();
            return _instance ?? null;
        }
    }
    #endregion
    
    #region serializeField

    [SerializeField] private GameObject noteGroupPrefab;
    [SerializeField] private GameObject gameSceneUI;
    
    #endregion

    #region Cache

    private GameObject _noteGroup;
    private NoteGroupScript _noteGroupScript;
    
    #endregion

    #region Unity Methods
    private void Awake()
    {
        CheckInit();
        if (!Init()) Debug.LogError($"{nameof(GameSceneInit)} init failed");
    }

    #endregion

    #region Methods
    
    private bool Init()
    {
        _noteGroup = GameObject.Instantiate(noteGroupPrefab);
        _noteGroupScript = _noteGroup.GetComponent<NoteGroupScript>();
        return _noteGroup is not null || _noteGroupScript is not null;
    }
    private void CheckInit()
    {
        var managers = FindObjectsByType<ManagerBase>(FindObjectsSortMode.None);
        if (managers.Length >= 1) return;
        // 생성되된 매니저가 없다면 Init씬으로 이동
        SceneLoadManager.instance.LoadScene(SceneType.Init);
    }

    public GameSceneUI GetGameSceneUI()
    {
        return gameSceneUI.GetComponent<GameSceneUI>();
    }
    
    public NoteGroupScript GetNoteGroupScript()
    {
        return _noteGroupScript;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
// using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : ManagerBase
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region SerializeField
    [SerializeField] private float gameTime = 120.0f; // 게임 시간
    #endregion

    #region Variables
    public int Score { get; private set; }
    public int Combo { get; private set; }
    public Timer Timer { get; private set; }
    public bool IsGameOver { get; private set; } = false;
    public bool IsGameReady { get; private set; } = false;
    public bool IsGameStart { get; private set; } = false;
    public bool IsGamePause { get; private set; } = false;
    public bool IsInit { get; private set; } = false;
    #endregion

    #region Cache
    private GameObject objNoteGroup = null;
    private NoteGroup_Script noteGroupScript = null;
    private GameSceneUI gameSceneUI = null;
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<GameManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (IsGameOver == true)
        {
            // 게임 오버 상태일 때 처리할 작업
            SceneManager.Instance.LoadScene(SCENE_TYPE.GAMEOVER);
            return;
        }
        // 게임 진행 중일 때 처리할 작업

    }
    #endregion

    #region Override Methods
    public override void Init()
    {
        // 게임 초기화
        Score = 0;
        Combo = 0;
        IsGameOver = false;


        // 노트 그룹 오브젝트 초기화
        objNoteGroup = GameObject.Find("NoteGroup");
        noteGroupScript = objNoteGroup.GetComponent<NoteGroup_Script>();
        if (noteGroupScript == null)
        {
            Debug.LogError("NoteGroup_Script not found.");
            return;
        }

        // 게임 UI 초기화
        gameSceneUI = FindAnyObjectByType<GameSceneUI>();
        if (gameSceneUI == null)
        {
            Debug.LogError("GameSceneUI not found.");
            return;
        }

        // // 타이머 초기화
        // timer = new Timer(60);
        // timer.Start();

        // 게임 시작
        IsGameStart = false;
        IsGamePause = false;
        IsGameReady = false;
        IsInit = true;
    }
    #endregion

    #region Custom Methods
    public void InputProcess(INPUT_TYPE inputType)
    {
        try
        {
            if (noteGroupScript == null)
            {
                Debug.LogError("NoteGroup_Script not found.");
                return;
            }

            // 입력 처리
            if (inputType == INPUT_TYPE.CATCH)
            {
                // Debug.Log("Have 버튼 클릭됨");
                if (noteGroupScript.GetNoteType(0) == NOTE_TYPE.APPLE)
                {
                    // Debug.Log("+10");
                    Score += 10;
                    // Debug.Log("콤보 +1");
                    Combo++;
                }
                else if (noteGroupScript.GetNoteType(0) == NOTE_TYPE.GOLDAPPLE)
                {
                    // Debug.Log("+20");
                    Score += 20;
                    // Debug.Log("콤보 +1");
                    Combo++;
                }
                else
                {
                    // Debug.Log("-15");
                    Score -= 15;
                    // Debug.Log("콤보 초기화");
                    Combo = 0;
                }
            }
            else if (inputType == INPUT_TYPE.THROW)
            {
                // Debug.Log("Throw 버튼 클릭됨");
                if (noteGroupScript.GetNoteType(0) != NOTE_TYPE.ROTTENAPPLE)
                {
                    // Debug.Log("-15");
                    Score -= 15;
                    // Debug.Log("콤보 초기화");
                    Combo = 0;
                }
            }
            noteGroupScript.NoteProcess();
            // Debug.Log("Score: " + Score);
            // Debug.Log("Combo: " + Combo);
            gameSceneUI.SetScore(Score);
            gameSceneUI.SetCombo(Combo);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error: " + e.Message);
            return;
        }
    }
    #endregion
}

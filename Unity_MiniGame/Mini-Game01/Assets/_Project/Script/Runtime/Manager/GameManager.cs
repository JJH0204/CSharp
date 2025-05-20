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
    // public bool IsGameOver { get; private set; } = false;
    // public bool IsGameReady { get; private set; } = false;
    // public bool IsGameStart { get; private set; } = false;
    // public bool IsGamePause { get; private set; } = false;
    public GAME_STATE GameState { get; private set; } = GAME_STATE.NONE;
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
        // 게임 씬이 아닐 경우 Update 처리하지 않음
        if (SceneLoadManager.Instance.CurrentSceneType != SCENE_TYPE.GAME)
            return;

        // 게임 매니저가 게임 씬에 도달했을 때 처리할 작업
        switch (GameState)
        {
            case GAME_STATE.NONE:
                GameState = GAME_STATE.INIT;
                break;
            case GAME_STATE.INIT:
                // 게임 준비 상태일 때 처리할 작업
                if (StartInit())
                    GameState = GAME_STATE.READY;
                break;
            case GAME_STATE.READY:
                // 게임 준비 상태일 때 처리할 작업
                // 게임 시작 전 3초 대기 카운트

                GameState = GAME_STATE.PLAYING;
                break;
            case GAME_STATE.PLAYING:
                // 게임 시작 상태일 때 처리할 작업
                gameSceneUI.SetTime(gameTime);
                // 게임 시간이 0이 되면 게임 오버 상태로 전환
                if (gameTime <= 0)
                {
                    GameState = GAME_STATE.GAMEOVER;
                }
                else
                {
                    gameTime -= Time.deltaTime;
                }
                Debug.Log("게임 시간: " + gameTime);

                break;
            case GAME_STATE.PAUSE:
                // 게임 일시 정지 상태일 때 처리할 작업
                break;
            case GAME_STATE.GAMEOVER:
                // 게임 오버 상태일 때 처리할 작업
                // 게임 오버 UI 표시
                SceneLoadManager.Instance.LoadScene(SCENE_TYPE.GAMEOVER);
                break;
            case GAME_STATE.END:
                // 게임 종료 상태일 때 처리할 작업
                break;
            default:
                // 예외 처리
                Debug.LogError("Invalid game state.");
                break;
        }
    }
    #endregion

    #region Override Methods
    public override bool Init()
    {
        // 게임 매니저 생성 확인
        IsInit = true;
        return true;
    }
    #endregion

    #region Custom Methods
    private bool StartInit()
    {
        if (SceneLoadManager.Instance.CurrentSceneType == SCENE_TYPE.GAME)
        {
            // 게임 초기화
            Score = 0;
            Combo = 0;

            // 노트 그룹 오브젝트 초기화
            StartCoroutine(WaitForNoteGroup());

            // 게임 UI 초기화
            StartCoroutine(WaitForGameSceneUI());

            // // 타이머 초기화
            // timer = new Timer(60);
            // timer.Start();

            // 모든 코루틴이 완료되면 true 반환
            if (objNoteGroup != null && noteGroupScript != null && gameSceneUI != null)
            {
                // Debug.Log("모든 초기화 완료");
                return true;
            }
            else
            {
                // Debug.Log("초기화 대기 중...");
                return false;
            }
        }
        return false;
    }
    
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

    #region Coroutines
    private IEnumerator WaitForNoteGroup()
    {
        while (GameObject.Find("NoteGroup") == null)
        {
            Debug.Log("Waiting for NoteGroup GameObject to be created...");
            yield return new WaitForSeconds(0.5f); // 0.5초 간격으로 확인
        }

        objNoteGroup = GameObject.Find("NoteGroup");
        noteGroupScript = objNoteGroup.GetComponent<NoteGroup_Script>();

        if (noteGroupScript == null)
        {
            Debug.LogError("NoteGroup_Script not found after NoteGroup creation.");
        }
        else
        {
            Debug.Log("NoteGroup and NoteGroup_Script successfully initialized.");
        }
    }
    private IEnumerator WaitForGameSceneUI()
    {

        while (FindAnyObjectByType<GameSceneUI>() == null)
        {
            Debug.Log("Waiting for GameSceneUI to be created...");
            yield return new WaitForSeconds(0.5f); // 0.5초 간격으로 확인
        }

        gameSceneUI = FindAnyObjectByType<GameSceneUI>();
        if (gameSceneUI == null)
        {
            Debug.LogError("GameSceneUI not found.");
        }
    }
    #endregion
}

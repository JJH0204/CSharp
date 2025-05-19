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

    #region Variables
    [SerializeField] private int nScore = 0;
    [SerializeField] private Timer timer = null;
    [SerializeField] private bool isGameOver = false;
    #endregion

    #region Cache
    private GameObject objNoteGroup = null;
    private NoteGroup_Script noteGroupScript = null;
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
        if (isGameOver == true)
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
        nScore = 0;
        isGameOver = false;

        // 노트 그룹 오브젝트 초기화
        objNoteGroup = GameObject.Find("NoteGroup");
        noteGroupScript = objNoteGroup.GetComponent<NoteGroup_Script>();
        
        // // 타이머 초기화
        // timer = new Timer(60);
        // timer.Start();
    }
    #endregion

    #region Custom Methods
    public void InputProcess(INPUT_TYPE inputType)
    {
        // 입력 처리
        if (inputType == INPUT_TYPE.CATCH)
        {
            // Have 버튼 클릭 시 처리할 작업
            Debug.Log("Have 버튼 클릭됨");
            // NoteSystemManager.Instance.OnInput_Func(keyCode);
            if (noteGroupScript == null)
            {
                Debug.LogError("NoteGroup_Script not found.");
                return;
            }

            if (noteGroupScript.GetNoteType(0) == NOTE_TYPE.APPLE)
            {
                Debug.Log("+10");
                Debug.Log("콤보 +1");
            }
            else if (noteGroupScript.GetNoteType(0) == NOTE_TYPE.GOLDAPPLE)
            {
                Debug.Log("+20");
                Debug.Log("콤보 +1");
            }
            else
            {
                Debug.Log("-15");
                Debug.Log("콤보 초기화");
            }
        }
        else if (inputType == INPUT_TYPE.THROW)
        {
            // Throw 버튼 클릭 시 처리할 작업
            Debug.Log("Throw 버튼 클릭됨");
            if (noteGroupScript.GetNoteType(0) == NOTE_TYPE.ROTTENAPPLE)
            {
                Debug.Log("+0");
                Debug.Log("콤보 +0");
            }
            else
            {
                Debug.Log("-15");
                Debug.Log("콤보 초기화");
            }
        }
        noteGroupScript.NoteProcess();
    }
    #endregion
}

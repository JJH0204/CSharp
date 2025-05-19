using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField]
    private int nScore = 0;
    private Timer timer = null;
    private bool isGameOver = false;
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

    }
    #endregion

    #region Override Methods
    public override void Init()
    {
        // 게임 초기화
        nScore = 0;
        isGameOver = false;

        // // 타이머 초기화
        // timer = new Timer(60);
        // timer.Start();
    }
    #endregion
}

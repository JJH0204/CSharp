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

    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion
}

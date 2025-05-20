using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : ManagerBase
{
    #region Singleton
    private static SceneLoadManager instance = null;
    public static SceneLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoadManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SceneLoadManager");
                    instance = obj.AddComponent<SceneLoadManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Variables
    public bool IsInit { get; private set; } = false;
    public SCENE_TYPE CurrentSceneType { get; private set; } = SCENE_TYPE.NONE;
    #endregion

    #region Override Methods
    public override bool Init()
    {
        // 씬 초기화
        CurrentSceneType = SCENE_TYPE.INIT;
        return IsInit = true;
    }
    #endregion

    #region Custom Methods
    public void LoadScene(SCENE_TYPE sceneType)
    {
        // 씬 로드 처리
        if (CurrentSceneType == sceneType)
        {
            Debug.Log($"Already in {sceneType} scene.");
            return;
        }
        CurrentSceneType = sceneType;
        // 씬 전환 처리
        // SceneManager.LoadScene(sceneType.ToString());
        SceneManager.LoadSceneAsync(CurrentSceneType.ToString());
        Debug.Log($"Loading {sceneType} scene...");
    }
    #endregion
}

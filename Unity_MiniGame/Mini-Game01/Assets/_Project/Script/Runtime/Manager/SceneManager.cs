using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : ManagerBase
{
    #region Singleton
    private static SceneManager instance = null;
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SceneManager");
                    instance = obj.AddComponent<SceneManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Override Methods
    public override void Init()
    {
        // 씬 초기화
    }
    #endregion

    #region Custom Methods
    public void LoadScene(SCENE_TYPE sceneType)
    {
        // 씬 로드 처리
        Debug.Log("씬 로드: " + sceneType);
    }
    #endregion
}

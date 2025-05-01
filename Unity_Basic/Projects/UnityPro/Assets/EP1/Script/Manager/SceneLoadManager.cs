using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : ManagerBase
{
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

    public bool IsInit { get; set; } = false;
    public SCENE_TYPE afterSceneType { get; set; }
    public SCENE_TYPE initSceneType { get; set; } = SCENE_TYPE.Lobby;
    private void Awake()
    {
        DontDestroy<SceneLoadManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }

    public void GoInitAndReturnScene(SCENE_TYPE sceneType)
    {
        this.initSceneType = sceneType;
        SceneManager.LoadScene(SCENE_TYPE.Init.ToString());
    }

    public void SceneLoad(SCENE_TYPE sceneType)
    {
        if (IsInit == false)
        {
            Debug.LogError("SceneLoadManager is not initialized.");
            return;
        }
        afterSceneType = sceneType;

        // SceneManager.LoadScene(sceneType.ToString());
        SceneManager.LoadSceneAsync(SCENE_TYPE.Loading.ToString());
    }
}

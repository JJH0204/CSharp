using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScene_Init : MonoBehaviour
{
    private void Awake()
    {
        // SystemManager systemManager = FindAnyObjectByType<SystemManager>();
        // if (systemManager == null)
        //     SceneManager.LoadScene(SCENE_TYPE.Init.ToString());

        if (!SystemManager.Instance.IsInit)
            SceneLoadManager.Instance.GoInitAndReturnScene(SCENE_TYPE.InGame);
        

    }
}

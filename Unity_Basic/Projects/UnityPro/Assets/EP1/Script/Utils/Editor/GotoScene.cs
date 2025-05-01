using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GotoScene : MonoBehaviour
{
    // Unity 프로젝트 상단바에 GoToScene 버튼을 추가합니다.
    [MenuItem("Scene/Fast Start")]
    public static void FastStart()
    {
        EditorSceneManager.OpenScene($"Assets/EP1/Scene/{SCENE_TYPE.InGame}.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Scene/GoToScene/Init")]
    public static void GoToScene_Init()
    {
        EditorSceneManager.OpenScene($"Assets/EP1/Scene/{SCENE_TYPE.Init}.unity");
    }

    [MenuItem("Scene/GoToScene/In Game")]
    public static void GoToScene_InGame()
    {
        EditorSceneManager.OpenScene($"Assets/EP1/Scene/{SCENE_TYPE.InGame}.unity");
    }

    [MenuItem("Scene/GoToScene/Lobby")]
    public static void GoToScene_Lobby()
    {
        EditorSceneManager.OpenScene($"Assets/EP1/Scene/{SCENE_TYPE.Lobby}.unity");
    }
}

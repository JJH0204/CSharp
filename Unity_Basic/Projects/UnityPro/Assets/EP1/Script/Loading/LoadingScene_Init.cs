using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene_Init : MonoBehaviour
{
    private LoadingScene_UI loadingSceneUI; // cache
    private SceneLoadManager sceneLoadManager; // cache
    private void Awake()
    {
        loadingSceneUI = FindAnyObjectByType<LoadingScene_UI>();
        sceneLoadManager = FindAnyObjectByType<SceneLoadManager>();
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneLoadManager.afterSceneType)); // 비동기 씬 로드 시작
    }
    public IEnumerator LoadSceneAsync(SCENE_TYPE sceneType)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneType.ToString());
        // asyncOperation.progress

        while (!asyncOperation.isDone)
        {
            // Debug.Log(asyncOperation.progress);
            loadingSceneUI.SetPercent(asyncOperation.progress / 0.9f); // 0.0f ~ 1.0f
            yield return null; // 다음 프레임까지 대기
        }
    }
}

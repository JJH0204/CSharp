using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    private const int PROGRESS_VALUE = 5;
    private int progressAddValue = 0;    // 초기화 진행률을 나타내는 변수
    private SystemManager systemManager;    // cache
    private EffectManager effectManager;    // cache
    private ObjPoolManager objPoolManager;  // cache
    private WindowManager windowManager;    // cache
    private SoundManager soundManager;      // cache
    private InitScene_UI initSceneUI;       // cache

    private void Awake()    // Scene이 로드되어 오브젝트들의 인스턴스화가 완료되면 호출되어 초기화에 필요한 메니저들을 캐싱한다.
    {
        // systemManager = FindAnyObjectByType<SystemManager>();
        // effectManager = FindAnyObjectByType<EffectManager>();
        // objPoolManager = FindAnyObjectByType<ObjPoolManager>();
        // windowManager = FindAnyObjectByType<WindowManager>();
        // soundManager = FindAnyObjectByType<SoundManager>();
        // initSceneUI = FindAnyObjectByType<InitScene_UI>();
    }

    private IEnumerator Start()    // Awake()가 끝나고 호출되어 초기화가 필요한 메니저들을 일괄 초기화 시킨다.
    {
        yield return null;  // 다음 프레임까지 대기하여 모든 오브젝트들이 초기화된 후에 메니저들을 캐싱한다.
        systemManager = FindAnyObjectByType<SystemManager>();
        effectManager = FindAnyObjectByType<EffectManager>();
        objPoolManager = FindAnyObjectByType<ObjPoolManager>();
        windowManager = FindAnyObjectByType<WindowManager>();
        soundManager = FindAnyObjectByType<SoundManager>();
        initSceneUI = FindAnyObjectByType<InitScene_UI>();

        StartCoroutine(C_Manager());    // 초기화 메니저 코루틴 시작
    }

    private IEnumerator C_Manager()
    {
        List<Action> actions = new List<Action>()
        {
            SystemManagerInit,
            EffectManagerInit,
            ObjPoolManagerInit,
            WindowManagerInit,
            SoundManagerInit,
            LoadScene
        };

        foreach (var action in actions)
        {
            yield return new WaitForSeconds(0.1f);    // 0.1초 대기
            action?.Invoke();    // 각 초기화 메서드 호출
            SetProgress();    // 진행률 UI 업데이트
        }
        // SystemManagerInit();
        // SetProgress();
        // yield return new WaitForSeconds(0.1f);    // 0.1초 대기
        // EffectManagerInit();
        // SetProgress();
        // yield return new WaitForSeconds(0.1f);    // 0.1초 대기
        // ObjPoolManagerInit();
        // SetProgress();
        // yield return new WaitForSeconds(0.1f);    // 0.1초 대기
        // WindowManagerInit();
        // SetProgress();
        // yield return new WaitForSeconds(0.1f);    // 0.1초 대기
        // SoundManagerInit();
        // SetProgress();
        // yield return new WaitForSeconds(0.1f);    // 0.1초 대기
    }

    private void SetProgress()
    {
        initSceneUI.SetPercent((float)++progressAddValue / (float)PROGRESS_VALUE);    // 초기화 진행률 UI 업데이트
    }

    // 초기화에 필요한 동작들 정의
    private void SystemManagerInit()
    {
        systemManager.SetInit();
    }

    private void EffectManagerInit()
    {
        effectManager.SetInit();
    }

    private void ObjPoolManagerInit()
    {
        objPoolManager.SetInit();
    }

    private void WindowManagerInit()
    {
        windowManager.SetInit();
    }

    private void SoundManagerInit()
    {
        soundManager.SetInit();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SCENE_TYPE.Lobby.ToString());    // 로비 씬으로 이동
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    // private bool isInit = false;    // 초기화가 완료되었는지 여부를 나타내는 변수
    private const int PROGRESS_VALUE = 4;
    private int progressAddValue = 0;    // 초기화 진행률을 나타내는 변수
    // private SystemManager systemManager;    // cache
    // private EffectManager effectManager;    // cache
    // private ObjPoolManager objPoolManager;  // cache
    // private WindowManager windowManager;    // cache
    // private SoundManager soundManager;      // cache
    private InitScene_UI initSceneUI;       // cache

    private void Awake()    // Scene이 로드되어 오브젝트들의 인스턴스화가 완료되면 호출되어 초기화에 필요한 메니저들을 캐싱한다.
    {
        initSceneUI = FindAnyObjectByType<InitScene_UI>();

        // if (!isInit)    // 초기화가 완료되지 않았다면
        // {
        //     isInit = true;
        //     // 메니저 객체 인스턴스를 생성성
        //     systemManager = new GameObject("SystemManager").AddComponent<SystemManager>();
        //     effectManager = new GameObject("EffectManager").AddComponent<EffectManager>();
        //     objPoolManager = new GameObject("ObjPoolManager").AddComponent<ObjPoolManager>();
        //     windowManager = new GameObject("WindowManager").AddComponent<WindowManager>();
        //     soundManager = new GameObject("SoundManager").AddComponent<SoundManager>();
        // }
        // else    // 초기화가 완료되었다면
        // {
        //     systemManager = FindAnyObjectByType<SystemManager>();
        //     effectManager = FindAnyObjectByType<EffectManager>();
        //     objPoolManager = FindAnyObjectByType<ObjPoolManager>();
        //     windowManager = FindAnyObjectByType<WindowManager>();
        //     soundManager = FindAnyObjectByType<SoundManager>();
        //     initSceneUI = FindAnyObjectByType<InitScene_UI>();
        // }
    }

    private void Start()    // Awake()가 끝나고 호출되어 초기화가 필요한 메니저들을 일괄 초기화 시킨다.
    {
        // yield return null;  // 다음 프레임까지 대기하여 모든 오브젝트들이 초기화된 후에 메니저들을 캐싱한다.
        // systemManager = FindAnyObjectByType<SystemManager>();
        // effectManager = FindAnyObjectByType<EffectManager>();
        // objPoolManager = FindAnyObjectByType<ObjPoolManager>();
        // windowManager = FindAnyObjectByType<WindowManager>();
        // soundManager = FindAnyObjectByType<SoundManager>();
        // initSceneUI = FindAnyObjectByType<InitScene_UI>();

        StartCoroutine(C_Manager());    // 초기화 메니저 코루틴 시작
    }

    private IEnumerator C_Manager()
    {
        List<Action> actions = new List<Action>()
        {
            SystemManagerInit,
            // EffectManagerInit,
            // ObjPoolManagerInit,
            // WindowManagerInit,
            // SoundManagerInit,
            SceneLoadManagerInit,
            NetworkManagerInit,
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
        // systemManager.SetInit();

        // Singleton 패턴으로 캐싱 없이 SystemManager 인스턴스에 접근하여 초기화
        SystemManager.Instance.SetInit();    // SystemManager의 SetInit() 메서드 호출
    }

    private void SceneLoadManagerInit()
    {
        SceneLoadManager.Instance.SetInit();
    }


    private void NetworkManagerInit()
    {
        NetworkManager.Instance.SetInit(apiUrl: Config.SERVER_API_URL);    // 서버 API URL을 설정하여 초기화
        NetworkManager.Instance.SendPacket();    // 서버에 연결 요청
    }


    private void EffectManagerInit()
    {
        // effectManager.SetInit();
    }

    private void ObjPoolManagerInit()
    {
        // objPoolManager.SetInit();
    }

    private void WindowManagerInit()
    {
        // windowManager.SetInit();
    }

    private void SoundManagerInit()
    {
        // soundManager.SetInit();
    }

    private void LoadScene()
    {
        // SceneManager.LoadScene(SCENE_TYPE.Loading.ToString());    // 로비 씬으로 이동
        SceneLoadManager.Instance.SetInit();
        SceneLoadManager.Instance.SceneLoad(SceneLoadManager.Instance.initSceneType);    // 로비 씬으로 이동
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    [SerializeField] private GameObject systemPopupMessagePrefab;    // 시스템 메시지 팝업 프리팹
    [SerializeField] private Transform parentPopupMessage;    // 시스템 메시지 팝업 부모 오브젝트
    private const int PROGRESS_VALUE = 4;
    private int progressAddValue = 0;    // 초기화 진행률을 나타내는 변수
    private InitScene_UI initSceneUI;       // cache

    private void Awake()    // Scene이 로드되어 오브젝트들의 인스턴스화가 완료되면 호출되어 초기화에 필요한 메니저들을 캐싱한다.
    {
        initSceneUI = FindAnyObjectByType<InitScene_UI>();
    }

    private void Start()    // Awake()가 끝나고 호출되어 초기화가 필요한 메니저들을 일괄 초기화 시킨다.
    {
        StartCoroutine(C_Manager());    // 초기화 메니저 코루틴 시작
    }

    private IEnumerator C_Manager()
    {
# if By_Call_Back
        NetworkManagerInit();
        yield return new WaitForSeconds(0.1f);
        SetProgress();
#else
        IEnumerator enumerator = NetworkManagerInit();    // 네트워크 초기화
        yield return StartCoroutine(enumerator);
        bool isNetworkInitSuccess = (bool)enumerator.Current;    // 네트워크 초기화 성공 여부
        if (isNetworkInitSuccess)
        {
            Debug.Log("NetworkManager Init Success");
        }
        else
        {
            Debug.Log("NetworkManager Init Failed"); // 서버 오류, 안내창 띄워주기기
            GameObject objPopupMessage = Instantiate(systemPopupMessagePrefab, parentPopupMessage);

            PopupMessageInfo popupMessageInfo = new PopupMessageInfo(PopupMessageType.ONE_BUTTON, "서버 오류", "서버에 연결할 수 없습니다.\n네트워크 상태를 확인해주세요.");
            PopupMessage popupMessage = objPopupMessage.GetComponent<PopupMessage>();
            popupMessage.OpenMessage(popupMessageInfo, null, () =>
            {
                Application.Quit();    // 앱 종료
            });
            yield break;    // 초기화 실패 시 코루틴 종료
        }

        yield return StartCoroutine(EtcManager());
#endif
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


#if By_Call_Back
    private void NetworkManagerInit()
    {
        NetworkManager.Instance.SetInit(apiUrl: Config.SERVER_APP_CONFIG_URL);    // 서버 API URL을 설정하여 초기화

        ApplicationConfigSendPacket sendPacket = new ApplicationConfigSendPacket(
            Config.SERVER_APP_CONFIG_URL,
            PACKET_NAME_TYPE.ApplicationConfig,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION);
        NetworkManager.Instance.C_SendPacket<ApplicationConfigReceivePacket>(sendPacket, AppConfig);    // 서버에 연결 요청
    }

    private void AppConfig(ReceivePacketBase receivePacketBase)
    {
        ApplicationConfigReceivePacket receivePacket = receivePacketBase as ApplicationConfigReceivePacket;
        if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.OK)    // 서버에서 응답이 성공적으로 왔다면
        {
            SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;    // 서버에서 내려준 주소를 사용
            Debug.Log("NetworkManager Init Success");
            StartCoroutine(EtcManager());
        }
        else
        {
            Debug.Log("NetworkManager Init Failed"); // 서버 오류, 안내창 띄워주기기
        }
    }
#else
    private IEnumerator NetworkManagerInit()
    {
        NetworkManager.Instance.SetInit(apiUrl: Config.SERVER_APP_CONFIG_URL);    // 서버 API URL을 설정하여 초기화

        ApplicationConfigSendPacket sendPacket = new ApplicationConfigSendPacket(
            Config.SERVER_APP_CONFIG_URL,
            PACKET_NAME_TYPE.ApplicationConfig,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION);
        IEnumerator coroutine = NetworkManager.Instance.C_SendPacket<ApplicationConfigReceivePacket>(sendPacket);
        StartCoroutine(coroutine);    // 서버에 연결 요청
        ApplicationConfigReceivePacket receivePacket = coroutine.Current as ApplicationConfigReceivePacket;

        if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.OK)    // 서버에서 응답이 성공적으로 왔다면
        {
            SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;    // 서버에서 내려준 주소를 사용
            yield return true;
        }
        else    // 서버에서 응답이 실패했다면
        {
            yield return false;
        }
        // NetworkManager.Instance.SendPacket(sendPacket);    // 서버에 연결 요청
    }
#endif
    private IEnumerator EtcManager()
    {
        List<Action> actions = new List<Action>()
        {
            SystemManagerInit,
            // EffectManagerInit,
            // ObjPoolManagerInit,
            // WindowManagerInit,
            // SoundManagerInit,
            SceneLoadManagerInit,
            LoadScene
        };

        foreach (var action in actions)
        {
            yield return new WaitForSeconds(0.1f);    // 0.1초 대기
            action?.Invoke();    // 각 초기화 메서드 호출
            SetProgress();    // 진행률 UI 업데이트
        }
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

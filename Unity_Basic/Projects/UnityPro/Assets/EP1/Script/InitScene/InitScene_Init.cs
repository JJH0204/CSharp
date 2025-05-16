using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class InitScene_Init : MonoBehaviour
{
    #region Variables
    [Header("Cache")]
    private InitScene_UI initSceneUI;
    private SystemManager systemManager;
    private LocalStoregeManager localStoregeManager;
    private NetworkManager networkManager;
    private PopupManager popupManager;

    [Header("Data")]
    private bool isInit = false;
    private bool isMaintenance = false;

    [Header("Prefab")]
    [SerializeField] private GameObject popupMessagePrefab;
    [SerializeField] private GameObject popupInputMessagePrefab;
    [SerializeField] private Transform parentPopupMessage;


    #endregion
    #region Unity Methods
    void Awake()
    {
        Init();
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(InitCheck());
    }
    #endregion

    #region Helper Methods
    private void Init()
    {
        initSceneUI = FindObjectOfType<InitScene_UI>();
        systemManager = SystemManager.Instance;
        localStoregeManager = LocalStoregeManager.Instance;
        networkManager = NetworkManager.Instance;
        popupManager = PopupManager.Instance;

        if (systemManager.IsInit == false)
        {
            systemManager.SetInit();
            localStoregeManager.SetInit();
            networkManager.SetInit(Config.SERVER_APP_CONFIG_URL);
            popupManager.SetInit(popupMessagePrefab, popupInputMessagePrefab, parentPopupMessage);
        }
    }

    private IEnumerator InitCheck()
    {
        // 네트워크 체크
        #region Network Check
        // 서버 체크
        IEnumerator enumerator = networkManager.NetworkManagerInit();
        yield return StartCoroutine(enumerator);

        // 네트워크 초기화 성공 여부
        bool isNetworkInitSuccess = (bool)enumerator.Current;
        if (!isNetworkInitSuccess)
        {
            // 연결 실패 시 팝업 메시지 출력
            popupManager.CreatePopupMessage(PopupMessageType.ONE_BUTTON, "서버 오류", "서버에 연결할 수 없습니다.\n네트워크 상태를 확인해주세요.", null, () =>
            {
                Application.Quit();    // 앱 종료
            });
            yield break;    // 코루틴 종료
        }
        #endregion

        // 서버 점검 체크
        #region Server Maintenance Check
        if (systemManager.developerIdAuthority == DEVELOPER_ID_AUTHORITY.NONE)
        {
            // 점검 팝업 띄우기
            // 해당 패킷에서 오류가 발생하더라도, 계속 진행하도록 설정
            IEnumerator maintenance = networkManager.CoroutineMaintenanceSendPacket();
            yield return StartCoroutine(maintenance);

            MaintenanceReceivePacket maintenanceReceivePacket = maintenance.Current is MaintenanceReceivePacket packet ? packet : null;

            if (maintenanceReceivePacket != null && maintenanceReceivePacket.isMaintenance) // 점검 중이라면
            {
                popupManager.CreatePopupMessage(PopupMessageType.ONE_BUTTON, maintenanceReceivePacket.Title, maintenanceReceivePacket.Contents, null, () =>
                {
                    Debug.Log("점검 중입니다.");
                    Application.Quit();    // 앱 종료
                });
                yield break;
            }
        }
        #endregion

        // 개발자 인증 체크
        #region Developer ID Check

        #endregion
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupManager : ManagerBase
{
    #region Singleton
    private static PopupManager instance = null;

    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PopupManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PopupManager");
                    instance = obj.AddComponent<PopupManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Variables
    private bool isInit = false;

    [Header("Cache")]
    private GameObject systemPopupMessagePrefab;
    private GameObject systemPopupInputMessagePrefab;
    private Transform parentPopupMessage;
    #endregion

    #region Unity Methods
    void Awake()
    {
        DontDestroy<PopupManager>();
    }
    #endregion

    #region Methods
    public void SetInit(GameObject popupMessagePrefab, GameObject popupInputMessagePrefab, Transform parentPopupMessage)
    {
        if (isInit)
            return;
        this.systemPopupMessagePrefab = popupMessagePrefab;
        this.systemPopupInputMessagePrefab = popupInputMessagePrefab;
        this.parentPopupMessage = parentPopupMessage;
        
        isInit = true;
    }

    public void CreatePopupMessage(PopupMessageType type, string title, string content, Action onConfirm = null, Action onCancel = null)
    {
        GameObject objPopupMessage = Instantiate(systemPopupMessagePrefab, parentPopupMessage);
        PopupMessageInfo popupMessageInfo = new PopupMessageInfo(type, title, content);
        PopupMessage popupMessage = objPopupMessage.GetComponent<PopupMessage>();
        popupMessage.OpenMessage(popupMessageInfo, onCancel, onConfirm);
    }

    public void DeveloperIdPopup()
    {
        GameObject objPopupInputMessage = Instantiate(systemPopupInputMessagePrefab, parentPopupMessage);

        PopupMessageInfo popupInputMessageInfo = new PopupMessageInfo(PopupMessageType.TWO_BUTTON, "개잘자 인증", "테스트를 위한 ID를 입력해주세요.");
        PopupInputMessage popupMessage = objPopupInputMessage.GetComponent<PopupInputMessage>();
        popupMessage.OpenMessage(popupInputMessageInfo, SystemManager.Instance.DevelopmentID, null, null);
    }
    #endregion
}

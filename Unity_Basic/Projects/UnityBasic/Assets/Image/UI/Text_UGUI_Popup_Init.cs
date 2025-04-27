using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_UGUI_Popup_Init : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform parentPopup;

    void Start()
    {
        UpdatePopup();
        
    }

    private void UpdatePopup()
    {
        GameObject obj = Instantiate(popupPrefab, parentPopup);
        PopupMessage popupMessage = obj.GetComponent<PopupMessage>();
        PopupMessageInfo popupMessageInfo = new PopupMessageInfo(type: PopupMessageType.TWO_BUTTON, title: "업데이트 안내", contents: "업데이트 하시겠습니까?");
        popupMessage.OpenMessage(popupMessageInfo, CancleUpdate, GoUpdate);
    }

    private void MaintenancePopup()
    {
        GameObject obj = Instantiate(popupPrefab, parentPopup);
        PopupMessage popupMessage = obj.GetComponent<PopupMessage>();
        PopupMessageInfo popupMessageInfo = new PopupMessageInfo(type: PopupMessageType.ONE_BUTTON, title: "점검 안내", contents: "서버 점검 중입니다.");
        popupMessage.OpenMessage(popupMessageInfo, null, GoMaintenance);
    }

    private void GoUpdate()
    {
        Debug.Log("업데이트 승인");
    }

    private void CancleUpdate()
    {
        Debug.Log("업데이트 취소");
        MaintenancePopup();
    }
    private void GoMaintenance()
    {
        Debug.Log("업데이트 취소");
    }
}

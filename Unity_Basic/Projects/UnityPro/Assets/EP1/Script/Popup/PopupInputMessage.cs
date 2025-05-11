using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class PopupInputMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtContents;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject oneButton;
    [SerializeField] private GameObject twoButton;
    private PopupMessageInfo popupMessageInfo;
    private Action cancleAction;
    private Action<string> okAction;
    public void OpenMessage(PopupMessageInfo popupMessageInfo, string value, Action cancleAction, Action<string> okAction)
    {
        this.popupMessageInfo = popupMessageInfo;
        this.cancleAction = cancleAction;
        this.okAction = okAction;

        this.txtTitle.text = popupMessageInfo.title;
        this.txtContents.text = popupMessageInfo.contents;
        this.inputField.text = value;

        oneButton.SetActive(false);
        twoButton.SetActive(false);

        if (popupMessageInfo.type == PopupMessageType.ONE_BUTTON)
        {
            oneButton.SetActive(true);
        }
        else if (popupMessageInfo.type == PopupMessageType.TWO_BUTTON)
        {
            twoButton.SetActive(true);
        }

        // gameObject.SetActive(true);
    }

    public void OnClick_Cancle()
    {
        cancleAction?.Invoke();
        Destroy(gameObject);
    }

    public void OnClick_OK()
    {
        okAction?.Invoke(inputField.text);
        Destroy(gameObject);
    }
}

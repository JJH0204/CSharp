using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PopupMessageType
{
    ONE_BUTTON,
    TWO_BUTTON,
}

public class PopupMessageInfo
{
    public readonly PopupMessageType type;
    public readonly string title;
    public readonly string contents;

    public PopupMessageInfo(PopupMessageType type, string title, string contents)
    {
        this.type = type;
        this.title = title;
        this.contents = contents;
    }
}
public class PopupMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtContents;

    [SerializeField] private GameObject oneButton;
    [SerializeField] private GameObject twoButton;
    private PopupMessageInfo popupMessageInfo;
    private Action cancleAction;
    private Action okAction;
    public void OpenMessage(PopupMessageInfo popupMessageInfo, Action cancleAction, Action okAction)
    {
        this.popupMessageInfo = popupMessageInfo;
        this.cancleAction = cancleAction;
        this.okAction = okAction;

        this.txtTitle.text = popupMessageInfo.title;
        this.txtContents.text = popupMessageInfo.contents;

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
        okAction?.Invoke();
        Destroy(gameObject);
    }
}

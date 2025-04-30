using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Transform parentWindow;
    [SerializeField] private GameObject prefabUpgrade;
    [SerializeField] private GameObject prefabShop;
    [SerializeField] private GameObject prefabFriend;
    [SerializeField] private GameObject prefabHelp;

    private WindowManager windowManager;

    void Start()
    {
        windowManager = FindAnyObjectByType<WindowManager>();
        // GameObject obj = Instantiate(prefabUpgrade, parentWindow);
        // GameObject obj = Instantiate(prefabShop, parentWindow);
        // GameObject obj = Instantiate(prefabFriend, parentWindow);
        // GameObject obj = Instantiate(prefabHelp, parentWindow);
    }

    public void OnClickUpgrade()
    {
        windowManager.ChangeWindow(prefabUpgrade, parentWindow);
    }

    public void OnClickShop()
    {
        windowManager.ChangeWindow(prefabShop, parentWindow);
    }

    public void OnClickFriend()
    {
        windowManager.ChangeWindow(prefabFriend, parentWindow);
    }

    public void OnClickHelp()
    {
        windowManager.ChangeWindow(prefabHelp, parentWindow);
    }
}

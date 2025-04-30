using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WindowInfo // 활성화된 윈도우 정보를 저장하는 클래스스
{
    public readonly GameObject prefab;
    public readonly GameObject gameObject;

    public WindowInfo(GameObject prefab, GameObject gameObject)
    {
        this.prefab = prefab;
        this.gameObject = gameObject;
    }
}

public class WindowManager : MonoBehaviour
{
    private List<WindowInfo> windows = new List<WindowInfo>();

    public GameObject NewWindow(GameObject prefab, Transform parent)
    {
        // foreach (GameObject window in windows)
        // {
        //     if (window.name.ToString() + "(Clone)" == prefab.name.ToString()) // 프리팹을 비교 조건으로 사용하니 이상하게 동작한다.
        //     {
        //         window.SetActive(true);
        //         return window;
        //     }
        // }

        GameObject obj = Instantiate(prefab, parent);
        obj.transform.SetAsLastSibling();
        windows.Add(new WindowInfo(prefab, obj));
        return obj;
    }

    public GameObject ChangeWindow(GameObject prefab, Transform parent)
    {
        foreach (var window in windows)
        {
            // Destroy(window);
            window.gameObject.SetActive(false);
        }

        foreach (var window in windows)
        {
            if (window.prefab == prefab) // 정보 관리를 위한 클래스를 만들어서 비교
            {
                window.gameObject.SetActive(true);
                window.gameObject.transform.SetAsLastSibling(); // 맨 위로 올리기 (중요)
                return window.gameObject;
            }
        }
        return NewWindow(prefab, parent);
    }
}

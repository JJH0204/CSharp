using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSState : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

#region Variables
    [Range(10, 150)]
    [SerializeField] private int nFontSize = 30;
    [SerializeField] private Color color = new Color(.0f, .0f, .0f, 1.0f);
    [SerializeField] private float fWidth, fHeight;
#endregion

    void OnGUI()
    {
#if DEBUG
        Rect position = new Rect(fWidth, fHeight, Screen.width, Screen.height);

        float fFPS = 1.0f / Time.deltaTime;
        float fMS = Time.deltaTime * 1000.0f;
        string strFPS = string.Format("{0:N1} FPS ({1:N1}ms)", fFPS, fMS);

        GUIStyle style = new GUIStyle();
        style.fontSize = nFontSize;
        style.normal.textColor = color;
        GUI.Label(position, strFPS, style);
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSceneUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private Slider LoadingSlider = null; // 로딩 슬라이더
    [SerializeField] private TMPro.TextMeshProUGUI LoadingText = null; // 로딩 텍스트
    #endregion

    #region Unity Methods
    void Start()
    {
        Init();
    }
    #endregion
    #region Custom Methods
    private void Init()
    {
        LoadingText = LoadingSlider.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        LoadingText.text = "Loading...";
        LoadingSlider.value = 0f;
    }
    public void SetLoadingText(string text)
    {
        LoadingText.text = text;
    }
    public void SetLoadingSlider(float value)
    {
        LoadingSlider.value = value;
    }
    #endregion
}

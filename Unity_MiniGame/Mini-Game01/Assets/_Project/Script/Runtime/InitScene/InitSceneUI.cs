using UnityEngine;
using UnityEngine.UI;

public class InitSceneUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private Slider loadingSlider; // 로딩 슬라이더
    private TMPro.TextMeshProUGUI _loadingText; // 로딩 텍스트
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
        _loadingText = loadingSlider.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        _loadingText.text = "Loading...";
        loadingSlider.value = 0f;
    }
    public void SetLoadingText(string text)
    {
        _loadingText.text = text;
    }
    public void SetLoadingSlider(float value)
    {
        loadingSlider.value = value;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    #region Variables
    [SerializeField] private Slider timeSlider = null; // 타이머 슬라이더
    [SerializeField] private Text timeText = null; // 타이머 텍스트
    [SerializeField] private float maxTime = 60f; // 최대 시간
    [SerializeField] private float currentTime = 0f; // 현재 시간
    [SerializeField] private bool isRunning = false; // 타이머 실행 여부
    #endregion

    #region Custom Methods
    // 타이머 초기화 메서드
    public void Init()
    {
        timeSlider.maxValue = maxTime;
        timeSlider.value = currentTime;
        timeText.text = currentTime.ToString("F2");
    }
    #endregion
}

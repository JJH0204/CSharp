using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeView : MonoBehaviour
{
    #region Variables
    private TextMeshProUGUI timeText = null; // 타이머 텍스트
    [SerializeField] private bool isRunning = false; // 타이머 실행 여부
    #endregion

    #region Unity Methods
    private void Start()
    {

    }
    #endregion

    #region Custom Methods
    // 타이머 초기화 메서드
    public void Init()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        timeText.text = "00:00.00";
    }
    // 타이머 시작 메서드
    public void StartTimer(float time)
    {
        if (isRunning) return;
        isRunning = true;
        StartCoroutine(TimerCoroutine(time));
    }
    // 타이머 정지 메서드
    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }
    // 타이머 리셋 메서드
    public void ResetTimer()
    {
        isRunning = false;
        StopAllCoroutines();
        timeText.text = "00:00.00";
    }

    // 타이머 코루틴 메서드
    private IEnumerator TimerCoroutine(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText(time - elapsedTime);
            yield return null;
        }
        isRunning = false;
    }
    // 타이머 텍스트 업데이트 메서드
    private void UpdateTimerText(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        float milliseconds = (time - (minutes * 60 + seconds)) * 1000;
        timeText.text = string.Format("{0:D2}:{1:D2}.{2:D3}", minutes, seconds, (int)milliseconds);
    }
    #endregion
}

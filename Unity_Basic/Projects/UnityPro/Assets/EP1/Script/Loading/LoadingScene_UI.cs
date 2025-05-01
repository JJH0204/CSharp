using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScene_UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textPercent;
    public void SetPercent(float factor)    // 0.0f ~ 1.0f
    {
        slider.value = factor;
        textPercent.text = (factor * 100).ToString("F0") + "%";
    }
}

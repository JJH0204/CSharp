using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitScene_UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textPercent;
    [SerializeField] public LoadingGear LoadingGear { get; set; }

    private void Awake()
    {
        LoadingGear = FindAnyObjectByType<LoadingGear>(FindObjectsInactive.Include);
    }
    public void SetPercent(float factor)    // 0.0f ~ 1.0f
    {
        slider.value = factor;
        textPercent.text = (factor * 100).ToString("F0") + "%";
    }
}

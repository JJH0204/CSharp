using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text textBtn;
    [Header("Slider")]
    [SerializeField] private Slider slider;
    [Header("Toggle")]
    [SerializeField] private Toggle toggleA;
    [SerializeField] private Toggle toggleB;
    [Header("InputField")]
    [SerializeField] private TMP_InputField inputID;
    [SerializeField] private TMP_InputField inputPW;

    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown dropdown;

    void Start()
    {

        textBtn.text = "게임을 시작하려면 아래 버튼을 클릭하세요.";
        button.GetComponentInChildren<TMP_Text>().text = "Let's Start!";


        slider.value = 0;
        slider.GetComponentInChildren<TMP_Text>().text = "0%";
        slider.interactable = false;    // Disable the slider initially

        Debug.Log(toggleA.isOn);
        Debug.Log(toggleB.isOn);

        inputID.text = "Player ID";
        inputPW.text = "Password";

        dropdown.ClearOptions();
        dropdown.options.Add(new TMP_Dropdown.OptionData("Option 1") { text = "A" });
        dropdown.options.Add(new TMP_Dropdown.OptionData("Option 2") { text = "B" });
        dropdown.options.Add(new TMP_Dropdown.OptionData("Option 3") { text = "C" });
        dropdown.options.Add(new TMP_Dropdown.OptionData("Option 4") { text = "D" });
        dropdown.value = 0;
    }
    public void OnClickStart()
    {
        Debug.Log("Start button clicked");
        // Add your start game logic here
    }

    void Update()
    {
        slider.value += Time.deltaTime * 10;
        slider.GetComponentInChildren<TMP_Text>().text = ((int)slider.value).ToString() + "%";

        if (slider.value >= 100)
        {
            slider.value = 100;
            slider.GetComponentInChildren<TMP_Text>().text = "100%";
        }


    }

    public void OnValueChange_ToggleA(bool isOn)
    {
        Debug.Log("Toggle A is " + (isOn ? "on" : "off"));
    }

    public void OnValueChange_ToggleB(bool isOn)
    {
        Debug.Log("Toggle B is " + (isOn ? "on" : "off"));
    }

    public void OnValueChange_Dropdown()
    {
        Debug.Log("Dropdown value changed to: " + dropdown.value);
        TMP_Dropdown.OptionData selectedOption = dropdown.options[dropdown.value];
        Debug.Log("Selected option: " + selectedOption.text);
    }
}

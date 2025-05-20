using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    #region Variables
    public int Combo { set; get; }
    #endregion

    #region Unity Methods
    void Update()
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Combo.ToString();
    }
    #endregion

    #region Custom Methods
    // 점수 초기화 메서드
    public void Init()
    {
        Combo = 0;
    }
    #endregion
}

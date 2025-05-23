using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    #region Variables
    public int Score { set; get; }
    #endregion

    #region Unity Methods
    void Update()
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Score.ToString();
    }
    #endregion

    #region Custom Methods
    // 점수 초기화 메서드
    public void Init()
    {
        Score = 0;
    }
    #endregion
}

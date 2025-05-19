using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    #region Variables
    [SerializeField] private int score = 0; // 점수
    [SerializeField] private int minScore = 0; // 최소 점수
    #endregion

    #region Custom Methods
    // 점수 초기화 메서드
    public void Init()
    {
        score = 0;
    }
    #endregion
}

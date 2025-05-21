public class UserData
{
    public int bestScore { get; private set; } = 0; // 최고 점수
    public int highCombo { get; private set; } = 0; // 최고 콤보 
    public int totalScore { get; set; } = 0; // 총 점수
    public int currentCombo { get; set; } = 0; // 현재 콤보

    public void Init(HistoryData instanceHistoryData)
    {
        bestScore = instanceHistoryData.BestScore;
        highCombo = instanceHistoryData.HighCombo;
    }
}
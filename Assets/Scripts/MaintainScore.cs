using UnityEngine;
using TMPro;

public class MaintainScore : MonoBehaviour
{
    public TMP_Text scoreNumberText;

    void Start()
    {
        ResetScore();
    }
    public void ResetScore()
    {
        scoreNumberText.text = "0";
    }
    public void AddScore(int addtoScore)
    {
        int newScore = int.Parse(scoreNumberText.text)+addtoScore;
        scoreNumberText.text = newScore.ToString();
    }
}

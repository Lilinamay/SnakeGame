using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static event Action IncreaseBoardSize;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI recordText;

    [SerializeField] int largeBoardThreshold;

    // our score and record can be publicly acessed outside the ScoreManager
    // class, but it can only be set INSIDE ScoreManager.
    public int score { get; private set; }
    public int record { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // At the beginning of the game load the record score if it exists
        if(LoadScore())
        {
            // find value attached to key "RECORD"
            record = PlayerPrefs.GetInt("RECORD");
        }
        else
        {
            // If the key "RECORD" is not in playerprefs, set our record to 0
            PlayerPrefs.SetInt("RECORD", 0);
            record = 0;
        }

        scoreText.text = 0 + "";
        recordText.text = record + "";
    }

    public void SaveScore()
    {
        // If our score is greater than the record, save it to PlayerPrefs
        if(score >= record)
        {
            PlayerPrefs.SetInt("RECORD", score);
        }
        PlayerPrefs.Save();
    }

    bool LoadScore() { return PlayerPrefs.HasKey("RECORD"); }

    public void ResetRecord()
    {
        ResetScore();
        PlayerPrefs.SetInt("RECORD", 0);
        record = 0;
        recordText.text = record + "";
    }


    public void ResetScore()
    {
        score = 0;
        scoreText.text = score + "";
    }

    public void IncrementScore()
    {
        score += 1;
        scoreText.text = score + "";
        if (score > record)
        {
            record = score;
            recordText.text = record + "";
            SaveScore();
        }

        if(score == largeBoardThreshold)
        {
            IncreaseBoardSize?.Invoke();
        }
    }
}

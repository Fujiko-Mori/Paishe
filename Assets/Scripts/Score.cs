using TMPro;
using UnityEngine;

public enum CatchType { common, rare, legendary};
public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] int scoreCommon;
    [SerializeField] int scoreRare;
    [SerializeField] int scoreLegendary;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void changeScore(CatchType catchtype)
    {
        if (catchtype == CatchType.common)
        {
            score += scoreCommon;
        } 
        else if (catchtype == CatchType.rare)
        {
            score += scoreRare;
        }
        else if (catchtype == CatchType.legendary)
        {
            score += scoreLegendary;
        }
    }
}

using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return score;
    }
}

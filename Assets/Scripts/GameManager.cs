using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Score score;
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject basicUI;
    [SerializeField] GameObject paisheur;
    [SerializeField] TextMeshProUGUI finalScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timeIsUp())
        {
            basicUI.SetActive(false);
            paisheur.SetActive(false);
            endMenu.SetActive(true);
            finalScoreText.text = "Score: " + score.GetScore().ToString();
        }
    }
}

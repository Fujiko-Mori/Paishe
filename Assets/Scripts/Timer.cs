using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float initialTimer;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timerTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerTime = initialTimer;
        timerText.text = timeToTimer(timerTime);
    }

    // Update is called once per frame
    void Update()
    {
        timerTime -= Time.deltaTime;
        timerText.text = timeToTimer(timerTime);
    }

    string timeToTimer(float time)
    {
        int minute = Mathf.FloorToInt(time / 60);
        int seconde = Mathf.FloorToInt(time % 60);
        string str = minute.ToString() + ":" + seconde.ToString();
        return str;
    }
}

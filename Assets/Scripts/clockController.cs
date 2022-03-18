using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class clockController : MonoBehaviour
{
    [SerializeField]
    private float inGameDayLength = 480f;            // 10 mins currently
    [SerializeField]
    private float day = 0.77f;                     // start time

    private float hoursInDay = 24f;
    private float minsInHour = 60f;
    private string hours;
    private string minutes;
    private float dayNormalised;

    public int helicopterTime;

    public TextMeshProUGUI clockText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / inGameDayLength;
        dayNormalised = day % 1.0f;


        hours = Mathf.Floor(dayNormalised * hoursInDay).ToString("00");
        minutes = Mathf.Floor(((dayNormalised * hoursInDay) % 1f) * minsInHour).ToString("00");
        clockText.text = hours + ":" + minutes;

        Debug.Log(dayNormalised * 24);

    }
}

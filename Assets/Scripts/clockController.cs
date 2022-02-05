using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class clockController : MonoBehaviour
{
    [SerializeField]
    private float inGameDayLength = 240f;            // In game time in real time
    [SerializeField]
    private float day = 0.125f;

    private float hoursInDay = 24f;
    private float minsInHour = 60f;
    private string hours;
    private string minutes;

    public TextMeshProUGUI clockText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / inGameDayLength;        // 1 day = inGameDayLength 
       
        float dayLengthNormalised = day % 1f;

        hours = Mathf.Floor(day * hoursInDay).ToString("00");
        minutes = Mathf.Floor(((day * hoursInDay) % 1f) * minsInHour).ToString("00");
        clockText.text = hours + ":" + minutes;

    }
}

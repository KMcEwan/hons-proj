using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class clockController : MonoBehaviour
{
    [SerializeField]
    private float inGameDayLength = 480f;            // 8 mins currently
    [SerializeField]
    private float day = 0.77f;                     // start time

    private float hoursInDay = 24f;
    private float minsInHour = 60f;
    private string hours;
    private string minutes;
    private float dayNormalised;

    public float helicopterTime;
    public bool helicopterTimeReached;
    public bool helicopterLeaveTimeReached;
    private float normDayForHelicopterStart;
    private float normDayForHelicopterLeave;
    public float helicopterLeaveTime;

    public TextMeshProUGUI clockText;



    // helicopter script

    [SerializeField] private helicopter heliScript;


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

        normDayForHelicopterStart = dayNormalised * 24;
        normDayForHelicopterStart *= 100;
        normDayForHelicopterStart = Mathf.Floor(normDayForHelicopterStart);
        normDayForHelicopterStart /= 100;


        normDayForHelicopterLeave = dayNormalised * 24;
        normDayForHelicopterLeave *= 100;
        normDayForHelicopterLeave = Mathf.Floor(normDayForHelicopterLeave);
        normDayForHelicopterLeave /= 100;


        // Debug.Log(normDayForHelicopterStart);
        if (normDayForHelicopterStart == helicopterTime)
        {
            helicopterTimeReached = true;
        }
        if (helicopterTimeReached)
        {
            heliScript.activateHelicopter();
        }

        if(normDayForHelicopterLeave == helicopterLeaveTime)
        {
            helicopterLeaveTimeReached = true;
        }

        if(helicopterLeaveTimeReached)
        {
            heliScript.helicopterLeave();
        }

    }
}

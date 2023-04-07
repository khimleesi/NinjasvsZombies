using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText = null;
    private float _startTime;

    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _startTime = Time.time;
    }

    void Update()
    {
        if (_timerText != null)
        {
            float t = 60 - (Time.time - _startTime);

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f1");

             _timerText.text = "Time left : " + minutes + ":" + seconds;
        }
    }
}

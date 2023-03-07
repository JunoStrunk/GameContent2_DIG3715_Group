using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public GameObject TimerUI;

    [SerializeField]
    float timeLeft = 60f;

    //private variables
    float _minutes;
    float _seconds;

    // Update is called once per frame

    void Update()
    {
		if (timeLeft >= 0.0f)
		{
			timeLeft -= Time.deltaTime;
            _minutes = Mathf.Floor(timeLeft / 60f);
            _seconds = timeLeft % 60;
			TimerText.text = string.Format("{0}:{1}", _minutes, _seconds.ToString("00"));
		}
        else
        {
            GameEventSys.current.TimerEnded();
            TimerUI.SetActive(false);
            this.enabled = false;
        }
    }
}
﻿using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class Timer : MonoBehaviour
    {
        private const int _maxTimer = 5 * 60;
        public static float StartTime = 0;
        private Text _timerText;

        public void Awake()
        {
            _timerText = GameObject.Find("Timer").GetComponent<Text>();
        }

        public void UpdateTimer()
        {
            float t = _maxTimer - StartTime++;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60) < 10 ?
                "0" + (t % 60).ToString("f0") :
                (t % 60).ToString("f0");

            _timerText.text = minutes + ":" + seconds;
        }


    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class Timer : MonoBehaviour
    {
        private const int _maxTimer = 5 * 60;
        private float _startTime;
        private Text _timerText = GameObject.Find("Timer").GetComponent<Text>();

        public void StartTimer()
        {
            _startTime = Time.time;
        }

        public void UpdateTimer()
        {
            float t = Time.time - _startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");

            _timerText.text = minutes + ":" + seconds;
        }



    }
}

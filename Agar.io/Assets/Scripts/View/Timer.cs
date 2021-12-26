using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class Timer : MonoBehaviour
    {
        public const int MaxTime = 2 * 60;
        public static float Time = 0;
        private Text _timerText;

        public void Awake()
        {
            _timerText = GameObject.Find("Timer").GetComponent<Text>();
        }

        public void UpdateTimer()
        {
            float t = MaxTime - Time++;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60) < 9 ?
                "0" + (t % 60).ToString("f0") :
                (t % 60).ToString("f0");

            _timerText.text = minutes + ":" + seconds;
        }


    }
}

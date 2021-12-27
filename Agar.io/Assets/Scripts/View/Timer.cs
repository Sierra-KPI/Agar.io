using UnityEngine;
using UnityEngine.UI;

namespace Agario.UnityView
{
    public class Timer : MonoBehaviour
    {
        #region Fields

        public const int MaxTime = 2 * 60;
        public static float Time = 0;
        private Text _timerText;
        private static string _timerName = "Timer";
        private static int _secondsPerMinute = 60;
        private string string _delimiter = ":";

        #endregion Fields

        #region Methods

        public void Awake()
        {
            _timerText = GameObject.Find(_timerName).GetComponent<Text>();
        }

        public void UpdateTimer()
        {
            float t = MaxTime - Time++;
            string minutes = ((int)t / _secondsPerMinute).ToString();
            string seconds = (t % _secondsPerMinute) < 9 ?
                "0" + (t % _secondsPerMinute).ToString("f0") :
                (t % _secondsPerMinute).ToString("f0");

            _timerText.text = minutes + _delimiter + seconds;
        }

        #endregion Methods
    }
}

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
        private static readonly string s_timerName = "Timer";
        private static readonly int s_secondsPerMinute = 60;
        private static readonly string s_delimiter = ":";

        #endregion Fields

        #region Methods

        public void Awake()
        {
            _timerText = GameObject.Find(s_timerName).GetComponent<Text>();
        }

        public void UpdateTimer()
        {
            float t = MaxTime - Time++;
            string minutes = ((int)t / s_secondsPerMinute).ToString();
            string seconds = (t % s_secondsPerMinute) < 9 ?
                "0" + (t % s_secondsPerMinute).ToString("f0") :
                (t % s_secondsPerMinute).ToString("f0");

            _timerText.text = minutes + s_delimiter + seconds;
        }

        #endregion Methods
    }
}

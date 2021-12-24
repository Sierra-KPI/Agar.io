using UnityEngine;

namespace Agario.UnityView
{
    public class Player : MonoBehaviour
    {
        public string Username;

        void Start()
        {
            gameObject.AddComponent<TextMesh>();
            gameObject.GetComponent<TextMesh>().text = Username;
        }
    }
}

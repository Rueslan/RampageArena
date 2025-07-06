using UnityEngine;

namespace Assets.Scripts.Common
{
    public class DontDestroyOnLoadScript : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}

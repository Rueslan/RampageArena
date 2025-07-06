using UnityEngine;

namespace Assets.Scripts.CFXR
{
    public class BloodSplashHandler : MonoBehaviour
    {
        [SerializeField] GameObject _splashPrefab;

        public void SplashBlood(Vector3 position, Vector3 direction)
        {
            position.y += 1.5f;
            Instantiate(_splashPrefab, position, Quaternion.LookRotation(direction));
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class StatusLabelHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _labelText;

        public void PlayStatusAnimation(string text)
        {
            gameObject.SetActive(true);
            _labelText.rectTransform.position = Vector3.zero;
            _labelText.text = text;
            StartCoroutine(labelAnimation());
        }

        private IEnumerator labelAnimation()
        {
            while (_labelText.rectTransform.position.y < 100)
            {
                _labelText.rectTransform.position += Vector3.up * 2;
                yield return new WaitForSeconds(0.01f);
            }
            gameObject.SetActive(false);
        }

    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class HPBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public TMP_Text HealthText;

        [SerializeField] private Gradient gradient;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;
            ImageCurrent.color = gradient.Evaluate(current / max);
            if (HealthText is not null)
            {
                HealthText.text = $"{current} %";
            }
        }
    }
}

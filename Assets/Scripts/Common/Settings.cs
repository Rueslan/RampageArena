using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Slider _soundSlider;

    private float _soundVolume;
    private int _quality; 

    private void Awake()
    {
        _soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        _quality = PlayerPrefs.GetInt("Quality", 2);

        _qualityDropdown.value = _quality;
        _soundSlider.value = _soundVolume;

    }

    public void SetVolume(float volume) => 
        _soundVolume = volume;

    public void SetQuality(int qualityIndex)
    {
        _quality = qualityIndex;
        QualitySettings.SetQualityLevel(_quality);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("SoundVolume", _soundVolume);
        PlayerPrefs.SetInt("Quality", _quality);
    }

}

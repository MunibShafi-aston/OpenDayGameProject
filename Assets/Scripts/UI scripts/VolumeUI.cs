using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider volumeSlider;

    void OnEnable() // 👈 important for pause menu
    {
        if (soundManager.Instance != null)
        {
            float current = PlayerPrefs.GetFloat("MasterVolume", 1f);

            volumeSlider.value = current;

            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    void OnVolumeChanged(float value)
    {
        soundManager.Instance.SetVolume(value);
    }
}

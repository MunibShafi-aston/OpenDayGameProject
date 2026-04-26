using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;


    public Slider volumeSlider;
    soundManager SoundManager;


    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;

        soundManager.Instance.PlayMusic("MainMenuMusic");

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

    }

    
    public void CharSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnVolumeChanged(float value)
    {
        soundManager.Instance.SetVolume(value);
    }
}

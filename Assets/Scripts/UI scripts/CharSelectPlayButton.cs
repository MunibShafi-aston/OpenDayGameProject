using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelectPlayButton : MonoBehaviour
{
    public void PlayGame()
    {
        if (CharacterSelection.Instance.HasSelectedCharacter())
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.LogWarning("No character selected! Please select a character before playing.");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{

    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.interactable = false;
    }

    public void EnablePlayButton()
    {
        playButton.interactable = true;
    }
    public void PlayButton()
    {
        if(!CharacterSelection.Instance.HasSelectedCharacter()){
            Debug.Log("No character selected!");
            return;
        }
        
        soundManager.Instance.PlayMusic("GameMusic"); 
        SceneManager.LoadScene(2);
    }
    
        public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    
}

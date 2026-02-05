using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelect : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }
    
        public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    
}

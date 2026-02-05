using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void CharSelect()
    {
        SceneManager.LoadScene(1);
    }
    
      public void SettingsButton()
    {
        SceneManager.LoadScene(3);
    }
}

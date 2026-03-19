using UnityEngine;
public class ControlMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject controlPanel;


    public void OpenControls()
    {
        controlPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlPanel.SetActive(false);
    }


}

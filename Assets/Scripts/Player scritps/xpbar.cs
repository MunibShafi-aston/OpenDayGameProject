using UnityEngine;
using UnityEngine.UI;

public class xpbar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMaxXP(float maxXP)
    {
        Debug.Log($"SetMaxXP called with {maxXP}");
        slider.maxValue = maxXP;
        slider.value = 0f;
    }

    public void UpdateXP(float currentXP, float maxXP)
    {
        Debug.Log($"UpdateXP called with currentXP={currentXP}, maxXP={maxXP}");
        slider.maxValue = maxXP;
        slider.value = currentXP;
    }
    public void SetXP(float currentXP)
    {
        slider.value = currentXP;
    }


}

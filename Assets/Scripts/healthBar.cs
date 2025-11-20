using UnityEngine;
using UnityEngine.UI;
public class healthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxHealth (int pHealth)
    {
        slider.maxValue = pHealth;
        slider.value = pHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth (int pHealth)
    {
        slider.value = pHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
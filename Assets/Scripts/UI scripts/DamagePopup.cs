using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    float moveSpeed = 20f;
    float lifetime = 1f;
    float fadeSpeed = 2f;
    Color textColor;

    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        textColor = text.color;
    }
    public void Setup(int damage)
    {  
        text.text = damage.ToString(); 
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        canvasGroup.alpha -= fadeSpeed * Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}

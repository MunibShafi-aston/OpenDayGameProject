using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    public float expandSpeed = 6f;
    public float maxRadius = 5f;
    public int damage = 2;

    CircleCollider2D col;
    float radius;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        radius = 0.1f;
    }

    void Update()
    {
        radius += expandSpeed * Time.deltaTime;
        transform.localScale = Vector3.one * radius * 2f;

        if(radius >= maxRadius)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth player = other.GetComponent<playerHealth>();
            if (player != null)
            {
                player.TakeDamageExternal(damage,"shockwave");
            }
        }
    }
}

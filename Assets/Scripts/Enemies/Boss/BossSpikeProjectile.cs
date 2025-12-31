using UnityEngine;

public class BossSpikeProjectile : MonoBehaviour
{

    public float speed = 8f;
    public float lifetime = 3f;
    public int damage = 1;

    Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerHealth health = other.GetComponent<playerHealth>();
        if (health != null)
        {
            health.TakeDamageExternal(damage);
            Destroy(gameObject);
        }
    }

}

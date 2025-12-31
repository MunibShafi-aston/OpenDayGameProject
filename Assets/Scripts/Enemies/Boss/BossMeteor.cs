using UnityEngine;

public class BossMeteor : MonoBehaviour
{
    public float fallSpeed = 12f;
    public int damage = 2;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.down *fallSpeed *Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth player = other.GetComponent<playerHealth>();
            if (player != null)
            {
                player.TakeDamageExternal(damage);
            }
            Destroy(gameObject);
        }
    }
}

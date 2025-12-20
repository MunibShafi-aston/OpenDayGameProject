using UnityEngine;

public class fireballProjectile : MonoBehaviour
{
    public float damage = 5f;
    public float speed = 5f;
    public float lifetime = 3f;


        void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Enemy e = col.GetComponent<Enemy>();
        if (e != null)
        {
            e.Health -= damage;
            Destroy(gameObject);
        }
    }
}

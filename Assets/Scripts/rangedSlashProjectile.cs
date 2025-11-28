using UnityEngine;

public class rangedSlashProjectile : MonoBehaviour
{ 
  Vector2 moveDirection;
    float moveSpeed;
    float life;

    public float damage = 3f;

    public void Setup(Vector2 dir, float speed, float lifetime, bool flipX)
    {
        moveDirection = dir.normalized;
        moveSpeed = speed;
        life = lifetime;

        Vector3 scale = transform.localScale;
        scale.x = flipX? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale; 
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        life -= Time.deltaTime;
        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     if (!other.CompareTag("Enemy")) return;

     Enemy enemy = other.GetComponent<Enemy>();
      if (enemy != null)
          {
         enemy.Health -= damage;
        }
    }
}

using UnityEngine;

public class BirdShieldOrbit : MonoBehaviour
{
    Transform player;

    float radius;
    float speed;
    float angle;
    float lifetime;

    public void Setup(Transform p, float r, float s, float startAngle, float duration)
    {
        player = p;
        radius = r;
        speed = s;
        angle = startAngle;
        lifetime = duration;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        angle += speed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0
        );

        transform.position = player.position + offset;

        Vector3 direction = (transform.position - player.position).normalized;

        float rotAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotAngle);
    }
    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponentInParent<Enemy>();
                if (enemy != null && !enemy.isDead)
                {
                    enemy.TankDamage(1f); 
                }
            }

            if (other.CompareTag("EnemyProjectile"))
            {
                Destroy(other.gameObject); 
            }
        }
    }


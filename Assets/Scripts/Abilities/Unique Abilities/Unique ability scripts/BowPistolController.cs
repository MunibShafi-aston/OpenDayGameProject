using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class BowPistolController : MonoBehaviour
{
    Transform player;
    public GameObject arrowPrefab;

    float radius;

    int arrowsPerBurst;
    float arrowDelay;

    float burstInterval;
    float lifetime;

    float baseDamage;

    Vector3 targetOffset;

    public float moveSpeed = 4f;
    public float changeTargetTime = 0.6f;

    PlayerStats stats;

    SpriteRenderer sprite;
    SpriteRenderer playerSprite;

    public void Setup(
        Transform p,
        float r,
        int arrows,
        float delay,
        float interval,
        float life,
        float dmg)
    {
        player = p;
        radius = r;

        arrowsPerBurst = arrows;
        arrowDelay = delay;

        burstInterval = interval;
        lifetime = life;

        baseDamage = dmg;

        stats = FindFirstObjectByType<PlayerStats>();

        sprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        PickNewOffset();

        StartCoroutine(RandomMovement());
        StartCoroutine(MainRoutine());

        Destroy(gameObject, lifetime);
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            PickNewOffset();
            yield return new WaitForSeconds(changeTargetTime);
        }
    }

    void PickNewOffset()
    {
        targetOffset = Random.insideUnitCircle * radius;
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPos = player.position + targetOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        RotateTowardCursor();

        if (playerSprite != null && sprite != null)
            sprite.flipX = playerSprite.flipX;
    }

    void RotateTowardCursor()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.z = 0;

        Vector3 dir = mouseWorld - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator MainRoutine()
    {
        float timer = 0f;

        while (timer < lifetime)
        {
            yield return new WaitForSeconds(burstInterval);

            StartCoroutine(ShootBurst());

            timer += burstInterval;
        }
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < arrowsPerBurst; i++)
        {
            FireArrow();
            yield return new WaitForSeconds(arrowDelay);
        }
    }

    void FireArrow()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.z = 0;

        Vector3 dir = (mouseWorld - transform.position).normalized;

        float playerDamage = stats != null ? stats.DealDamage() : 0f;

        float finalDamage = baseDamage + playerDamage;

        GameObject arrow = Instantiate(
            arrowPrefab,
            transform.position,
            Quaternion.identity
        );

        BowPistolArrow proj = arrow.GetComponent<BowPistolArrow>();
        proj.Setup(dir, finalDamage);
    }
}

using UnityEngine;
using System.Collections;

public class BossDashBurst : MonoBehaviour
{
    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.3f;
    public float chargeTime = 0.5f;

    [Header("Spike burst")]
    public GameObject spikePrefab;
    public int spikeCount = 12;
    public float spikeSpawnOffset = 0.5f;

    Rigidbody2D rb;
    Transform player;
    bool isAttacking;

    SpriteRenderer sr;
    int facingDirection = -1;

   void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>(); // 👈 ADD THIS
    }

    public void StartDashBurst()
    {
        if (isAttacking) return;
        StartCoroutine(DashBurstRoutine());
    }
    

    IEnumerator DashBurstRoutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;


        Vector2 rawDir = (player.position - transform.position);

        facingDirection = rawDir.x > 0 ? 1 : -1;
        sr.flipX = (facingDirection == 1);
        Vector2 dashDir = rawDir.normalized;

        rb.linearVelocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;

        SpawnSpikes();

        isAttacking = false;
    }

    void SpawnSpikes()
    {
        float angleStep = 360f / spikeCount;

        for (int i = 0; i < spikeCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            Vector2 spawnPos = (Vector2)transform.position + dir * spikeSpawnOffset;

            GameObject spike = Instantiate(spikePrefab, spawnPos, Quaternion.identity);
            spike.GetComponent<BossSpikeProjectile>().Init(dir);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttacking) return;

        if (collision.collider.CompareTag("Player"))
        {
            playerHealth health = collision.collider.GetComponent<playerHealth>();
            if (health != null)
            {
                health.TakeDamageExternal(2);
            }
        }
    }
}


using UnityEngine;
using System.Collections;

public class BossGroundPound : MonoBehaviour
{
    [Header("Jump")]
    public float jumpHeight = 3f;
    public float jumpDuration = 0.4f;

    [Header("Slam")]
    public float slamRadius = 3f;
    public int slamDamage = 0;

    [Header("Shockwave")]
    public GameObject shockwavePrefab;

    Rigidbody2D rb;
    Vector2 startPosition;
    bool isSlam = false;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartGroundPound()
    {
        if (isSlam) return;
        StartCoroutine(GroundPoundRoutine());    
    }

    IEnumerator GroundPoundRoutine()
    {
        isSlam = true;


        startPosition = rb.position;
        Vector2 peakPosition = startPosition + Vector2.up * jumpHeight;

        float t = 0f;
        while(t <jumpDuration)
        {
            t += Time.deltaTime;
            float y = Mathf.Lerp(0,jumpHeight, t/jumpDuration);
            rb.MovePosition(startPosition + Vector2.up * y);
            yield return null;
        }

        t = 0f;
        while (t < jumpDuration)
        {
            t += Time.deltaTime;
            rb.MovePosition(Vector2.Lerp(peakPosition, startPosition, t / jumpDuration));
            yield return null;
        }

        SlamDamage();
        SpawnShockwave();

        isSlam = false;
    }

    public void SlamDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            slamRadius
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerHealth player = hit.GetComponent <playerHealth>();
                if (player != null)
                {
                    player.TakeDamageExternal(slamDamage);
                }
            }
        }
    }

    void SpawnShockwave()
    {
        if(shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }

}
